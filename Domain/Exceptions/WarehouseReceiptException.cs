
using System;

namespace Domain.Exceptions
{
    public class WarehouseReceiptException:BaseException
    {
        public WarehouseReceiptException(
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
