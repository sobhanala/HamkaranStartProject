// DatabaseException.cs

using System;
using Microsoft.Extensions.Logging;

namespace Domain.Exceptions
{
    public class DatabaseException : BaseException
    {
        public DatabaseException(
            string technicalMessage,
            string userFriendlyMessage,
            ErrorCode errorCode,
            Exception innerException = null)
            : base(
                technicalMessage,
                userFriendlyMessage,
                errorCode
                )
        {
        }
    }
}