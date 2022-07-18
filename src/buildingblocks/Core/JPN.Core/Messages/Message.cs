using System;

namespace JPN.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; set; }
        public Guid AggredateId { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
