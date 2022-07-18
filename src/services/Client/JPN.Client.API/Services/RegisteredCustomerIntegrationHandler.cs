using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using FluentValidation.Results;
using JPN.Core.Mediator;
using JPN.Core.Messages.Integration;
using JPN.Customer.API.Application.Commands;
using JPN.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JPN.Customer.API.Services
{
    public class RegisteredCustomerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;
        public RegisteredCustomerIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RespondAsync<RegisteredUserIntegrationEvent, ResponseMessage>(async request =>
                await RegisterCustomer(request));

            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> RegisterCustomer(RegisteredUserIntegrationEvent message)
        {
            var customerCommand = new CustomerRegisterCommand(message.Id, message.Name, message.Email, message.Cpf, message.Phone);
            ValidationResult sucess;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucess = await mediator.SendCommand(customerCommand);
            }

            return new ResponseMessage(sucess);
        }
    }
}
