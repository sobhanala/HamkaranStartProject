using System;

namespace Domain.Exceptions
{
    public class AuthenticationException : BaseException
    {
        public AuthenticationException(
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