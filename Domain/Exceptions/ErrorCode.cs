namespace Domain.Exceptions
{
    public enum ErrorCode
    {
        /// validation Errors
        UserNameEmpty = 0,
        PasswordEmpty,
        EmailEmpty,
        


        /// Authentication Errors
        UserExist=10,
        PasswordWrong=11,
        PartyNotFound,

        /// Data base Errors 
        DataBaseError=50,

        Internal = 20

    }
}