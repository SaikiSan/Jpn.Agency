using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using JPN.Core.Messages;
using JPN.Customer.API.Application.Events;
using JPN.Customer.API.Models;
using JPN.Customer.API.Models.Interfaces;
using MediatR;

namespace JPN.Customer.API.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler,
        IRequestHandler<CustomerRegisterCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<ValidationResult> Handle(CustomerRegisterCommand message, CancellationToken cancellationToken)
        {
            if (!message.ItsValid()) return message.ValidationResult;

            var customer = new Models.CustomerModel(message.Id, message.Name, message.Cpf, message.Email, message.Phone);

            var existsCustomer = await _customerRepository.GetByCpf(customer.Cpf.Number);

            if (existsCustomer != null)
            {
                AddErrors("Cpf уже используется");
                return ValidationResult;
            }

            _customerRepository.Add(customer);

            customer.AddEvent(new CustomerRegisteredEvent(message.Id, message.Name, message.Email, message.Cpf, message.Phone));

            return await PersistData(_customerRepository.UnitOfWork);
        }
    }
}
