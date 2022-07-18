using System;
using FluentValidation;
using JPN.Core.DomainObjects;

namespace JPN.Customer.API.Application.Commands
{
    public class ClientRegisterValidation : AbstractValidator<CustomerRegisterCommand>
    {
        public ClientRegisterValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Пустое поле идентификатора");

            RuleFor(c => c.Email)
                .Must(ValidateEmail)
                .WithMessage("Неверный адрес электронной почты");

            RuleFor(c => c.Cpf)
                .Must(ValidateCpf)
                .WithMessage("Неверный CPF");

        }

        protected static bool ValidateCpf(string cpf)
        {
            return Cpf.Validate(cpf);
        }

        protected static bool ValidateEmail(string email)
        {
            return Email.Validate(email);
        }
    }
}