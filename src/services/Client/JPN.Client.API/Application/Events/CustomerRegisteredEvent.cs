using System;
using JPN.Core.Messages;
using MediatR;

namespace JPN.Customer.API.Application.Events
{
    public class CustomerRegisteredEvent : Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }

        public CustomerRegisteredEvent(Guid id, string name, string email, string cpf, string phone)
        {
            AggredateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
            Phone = phone;
        }
    }
}
