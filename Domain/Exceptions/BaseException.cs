using System;

namespace Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
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

        public ExceptionSeverity Severity { get; }
        public string UserFriendlyMessage { get; }
        public ErrorCode ErrorCode { get; }

        public string GetUserMessage()
        {
            return $"{UserFriendlyMessage} (Error Code: {ErrorCode})";
        }
    }
}