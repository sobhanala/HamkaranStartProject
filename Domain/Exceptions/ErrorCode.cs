using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public enum ErrorCode
    {
        ///
        /// validation Errors
        ///
        UserNameEmpty=0,
        PasswordEmpty ,






        ///
        /// Authentication Errors
        ///
        UserExist,
        PasswordWrong
        


    }
}
