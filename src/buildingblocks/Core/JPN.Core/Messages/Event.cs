using System;
using MediatR;

namespace JPN.Core.Messages
{
    public class Event : Message, INotification
    {
        public DateTime TimeStamp { get; set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
