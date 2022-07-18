using System;
using FluentValidation.Results;
using MediatR;

namespace JPN.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime TimeStamp { get; set; }
        public ValidationResult ValidationResult{ get; set; }

        public Command()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool ItsValid()
        {
            throw new NotImplementedException();
        }

    }
}
