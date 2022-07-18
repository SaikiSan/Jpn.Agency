using System.Threading.Tasks;
using FluentValidation.Results;
using JPN.Core.Data;

namespace JPN.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }


        protected void AddErrors(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AddErrors("Houve um erro ao persistir dados");

            return ValidationResult;
        }
    }
}
