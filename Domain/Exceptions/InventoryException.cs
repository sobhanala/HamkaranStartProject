using System;

namespace Domain.Exceptions
{
    public class InventoryException : BaseException
    {
        public InventoryException(
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