using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace everydayhero.Api
{
    [DebuggerStepThrough]
    public static class SecurityExtensions
    {
        public static string ToInsecureString(this SecureString secureString)
        {
            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static SecureString ToSecureString(this string insecureString)
        {
            var secureString = new SecureString();
            foreach (var character in insecureString.ToCharArray())
            {
                secureString.AppendChar(character);
            }
            return secureString;
        }
    }
}