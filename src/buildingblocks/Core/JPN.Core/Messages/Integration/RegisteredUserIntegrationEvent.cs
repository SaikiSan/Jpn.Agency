using System;

namespace JPN.Core.Messages.Integration
{
    public class RegisteredUserIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }

        public RegisteredUserIntegrationEvent(Guid id, string name, string email, string cpf, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
            Phone = phone;
        }
    }
}