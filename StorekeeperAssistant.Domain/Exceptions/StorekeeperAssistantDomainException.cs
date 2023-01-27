using System;

namespace StorekeeperAssistant.Domain.Exceptions
{
    /// <summary> Исключение  </summary>
    public class StorekeeperAssistantDomainException : Exception
    {
        public StorekeeperAssistantDomainException()
        { }

        public StorekeeperAssistantDomainException(string message)
            : base(message)
        { }

        public StorekeeperAssistantDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
