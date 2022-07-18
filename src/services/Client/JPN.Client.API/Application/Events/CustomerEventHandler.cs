using System.Threading;
using System.Threading.Tasks;
using JPN.EmailService;
using JPN.EmailService.Model;
using MediatR;

namespace JPN.Customer.API.Application.Events
{
    public class CustomerEventHandler : INotificationHandler<CustomerRegisteredEvent>
    {
        private readonly IEmailService _email;

        public CustomerEventHandler(IEmailService email)
        {
            _email = email;
        }

        public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _email.SendEmailAsync(new EmailModel
            {
                Destinatario = notification.Email,
                Assunto = "Добро пожаловать на наш сайт",
                Conteudo = "Теперь вы являетесь частью группы JPN Agency, которая заботится о самом мощном аниме из когда-либо созданных."
            });

            return Task.CompletedTask;
        }
    }
}
