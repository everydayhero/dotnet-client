using System;

namespace everydayhero.Api.Exceptions
{
    public class AuthenticationFailedException : UnauthorizedAccessException
    {
        public AuthenticationFailedException(AuthenticationErrorResult result)
        {
            Result = result;
        }

        public AuthenticationErrorResult Result { get; set; }
    }
}