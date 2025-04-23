using System;

namespace Domain.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(
            string technicalMessage,
            string userFriendlyMessage,
            ErrorCode errorCode,
            Exception innerException = null)
            : base(
                technicalMessage,
                userFriendlyMessage,
                errorCode)
        {
        }
    }
}