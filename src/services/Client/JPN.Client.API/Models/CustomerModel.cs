using System;
using JPN.Core.DomainObjects;

namespace JPN.Customer.API.Models
{
    public class CustomerModel : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public Cpf Cpf { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; set; }
        public bool Active { get; set; }

        protected CustomerModel() { }

        public CustomerModel(Guid id, string name, string cpf, string email, string number)
        {
            Id = id;
            Name = name;
            Cpf = new Cpf(cpf);
            Email = new Email(email);
            Phone = new Phone(number);
            Active = true;
        }

        public void TrocarEmail(string email)
        {
            Email = new Email(email);
        }
    }
}
