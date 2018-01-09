#region Imported Types

using System;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace DeviceSQL.ControlPanel.Security
{
    static class SecurePasswordManager
    {

        #region Password Protection Methods

        public static string ProtectPassword(this string password)
        {
            return Convert.ToBase64String(ProtectedData.Protect(Encoding.Unicode.GetBytes(password), null, DataProtectionScope.CurrentUser));
        }

        public static string UnprotectPassword(this string password)
        {
            return Encoding.Unicode.GetString(ProtectedData.Unprotect(Convert.FromBase64String(password), null, DataProtectionScope.CurrentUser));
        }

        #endregion

    }
}
