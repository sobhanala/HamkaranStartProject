namespace Domain.Exceptions
{
    public enum ErrorCode
    {
        /// validation Errors
        UserNameEmpty = 0,
        PasswordEmpty,


        /// Authentication Errors
        UserExist,
        PasswordWrong
    }
}