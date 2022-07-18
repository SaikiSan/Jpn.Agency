using JPN.WebAPI.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JPN.WebAPI.Core.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : MainController
    {
        [Route("error/{id:length(3,3)}")]
        public IActionResult ErrorRoutes(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 401)
            {
                modelErro.Mensagem =
                    "Você não tem permissão para essa operação Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Sem Permissão";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem =
                    "A página que está procurando não existe! Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return StatusCode(modelErro.ErroCode, modelErro);
        }
    }
}
