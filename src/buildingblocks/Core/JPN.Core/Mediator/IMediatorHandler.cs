using System.Threading.Tasks;
using FluentValidation.Results;
using JPN.Core.Messages;

namespace JPN.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
