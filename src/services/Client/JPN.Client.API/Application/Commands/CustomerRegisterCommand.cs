using System;
using JPN.Core.Messages;

namespace JPN.Customer.API.Application.Commands
{
    public class CustomerRegisterCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string  Email { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }

        public CustomerRegisterCommand(Guid id, string name, string email, string cpf, string phone)
        {
            AggredateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
            Phone = phone;
        }

        public override bool ItsValid()
        {
            ValidationResult = new ClientRegisterValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
