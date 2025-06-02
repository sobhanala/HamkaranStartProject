namespace Domain.Exceptions
{
    public enum ErrorCode
    {
        /// validation Errors
        UserNameEmpty = 0,
        PasswordEmpty,
        EmailEmpty,



        /// Authentication Errors
        UserExist = 10,
        PasswordWrong = 11,
        PartyNotFound,

        /// Data base Errors 
        DataBaseError = 50,


        /// <summary>
        /// here the Error  of recite declaring 
        /// </summary>
        InssufientProduct=60,
        NoStockFound=61,
        NoInventoryFound=62,

        /// <summary>
        /// 
        /// </summary>
        WerHouseTransactionFailed=70





        Internal = 20

    }
}