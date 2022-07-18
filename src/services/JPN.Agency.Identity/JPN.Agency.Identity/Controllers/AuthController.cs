using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using JPN.Authorization.API.Extensions;
using JPN.Authorization.API.Models;
using JPN.Core.Messages.Integration;
using JPN.MessageBus;
using JPN.WebAPI.Core.Controllers;
using JPN.WebAPI.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace JPN.Authorization.API.Controllers
{
    [Route("authorization/")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        private readonly IMessageBus _bus;

        public AuthController(SignInManager<IdentityUser> signInManager,
                             UserManager<IdentityUser> userManager, 
                             IOptions<AppSettings> appSettings,
                             IMessageBus bus)
        {
            _bus = bus;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Register(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResult(ModelState);

            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                var customerResult = await RegisterCustomer(usuarioRegistro);

                if (!customerResult.ValidationResult.IsValid)
                {
                    await _userManager.DeleteAsync(user);
                    return CustomResult(customerResult.ValidationResult);
                }

                return CustomResult(await GenerateJwt(usuarioRegistro.Email));
            }

            foreach (var error in result.Errors) AddErrors(error.Description);

            return CustomResult();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResult(ModelState);

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, 
                usuarioLogin.Senha, false, true);

            if (result.Succeeded) return CustomResult(await GenerateJwt(usuarioLogin.Email));

            if (result.IsLockedOut)
            {
                AddErrors("пользователь заблокирован за неправильные попытки");
                return CustomResult();
            }
            AddErrors("Неверный пароль или имя пользователя");
            return CustomResult();
        }

        private async Task<UsuarioRespostaLogin> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GetUserClaims(claims, user);
            var encodedToken = CodeToken(identityClaims);

            return GetTokenResponse(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> GetUserClaims(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF32.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UsuarioRespostaLogin GetTokenResponse(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationTime).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        private async Task<ResponseMessage> RegisterCustomer(UsuarioRegistro usuarioRegistro)
        {
            var user = await _userManager.FindByNameAsync(usuarioRegistro.Email);

            var userRegistered = new RegisteredUserIntegrationEvent(
                Guid.Parse(user.Id), usuarioRegistro.Name, usuarioRegistro.Email, usuarioRegistro.Cpf,
                usuarioRegistro.Phone);

            try
            {
                return await _bus.RequestAsync<RegisteredUserIntegrationEvent, ResponseMessage>(userRegistered);
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }
    }
}

