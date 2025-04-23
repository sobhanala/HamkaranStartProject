using System;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    
    public abstract class BaseException : Exception
    {
        public ExceptionSeverity Severity { get; }
        public string UserFriendlyMessage { get; }
        public ErrorCode ErrorCode { get; }

        protected BaseException(
            string message,
            string userFriendlyMessage,
            ErrorCode errorCode,
            ExceptionSeverity severity = ExceptionSeverity.Error,
            Exception innerException = null)
            : base(message, innerException)
        {
            Severity = severity;
            UserFriendlyMessage = userFriendlyMessage;
            ErrorCode = errorCode;
        }

        public string GetUserMessage()
        {
            return $"{UserFriendlyMessage} (Error Code: {ErrorCode})";
        }
    }
}