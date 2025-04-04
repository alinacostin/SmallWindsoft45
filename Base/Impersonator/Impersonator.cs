using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices; // DllImport
using System.Security.Principal; // WindowsImpersonationContext
using System.Security.Permissions; // PermissionSetAttribute

namespace Base.Impersonator
{


    //http://www.codeproject.com/KB/cs/cpimpersonation1.aspx
    // group type enum
    public enum SECURITY_IMPERSONATION_LEVEL : int
    {
        SecurityAnonymous = 0,
        SecurityIdentification = 1,
        SecurityImpersonation = 2,
        SecurityDelegation = 3
    }
    public class Impersonate
    {
        private System.Security.Principal.WindowsImpersonationContext newUser;

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        // closes open handes returned by LogonUser
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        // creates duplicate token handle
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool DuplicateToken(IntPtr ExistingTokenHandle,
            int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);

        public WindowsImpersonationContext ImpersonateUser(string sUsername, string sDomain, string sPassword)
        {
            // initialize tokens
            IntPtr pExistingTokenHandle = new IntPtr(0);
            IntPtr pDuplicateTokenHandle = new IntPtr(0);
            pExistingTokenHandle = IntPtr.Zero;
            pDuplicateTokenHandle = IntPtr.Zero;

            // if domain name was blank, assume local machine
            if (sDomain == "")
                sDomain = System.Environment.MachineName;

            try
            {
                string sResult = null;

                const int LOGON32_PROVIDER_DEFAULT = 0;
                const int LOGON32_LOGON_NEW_CREDENTIALS = 9;
                // create token
                //const int LOGON32_LOGON_INTERACTIVE = 2;
                //const int SecurityImpersonation = 2;

                // get handle to token
                bool bImpersonated = LogonUser(sUsername, sDomain, sPassword,
                    LOGON32_LOGON_NEW_CREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref pExistingTokenHandle);

                // did impersonation fail?
                if (false == bImpersonated)
                {
                    int nErrorCode = Marshal.GetLastWin32Error();
                    sResult = "LogonUser() failed with error code: " + nErrorCode + "\r\n";

                    // show the reason why LogonUser failed
                    throw new Exception(sResult);
                }

                // Get identity before impersonation
                sResult += "Before impersonation: " + WindowsIdentity.GetCurrent().Name + "\r\n";

                bool bRetVal = DuplicateToken(pExistingTokenHandle, (int)SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, ref pDuplicateTokenHandle);

                // did DuplicateToken fail?
                if (false == bRetVal)
                {
                    int nErrorCode = Marshal.GetLastWin32Error();
                    CloseHandle(pExistingTokenHandle); // close existing handle
                    sResult += "DuplicateToken() failed with error code: " + nErrorCode + "\r\n";

                    // show the reason why DuplicateToken failed
                    throw new Exception(sResult);

                }
                else
                {
                    // create new identity using new primary token
                    WindowsIdentity newId = new WindowsIdentity(pDuplicateTokenHandle);
                    WindowsImpersonationContext impersonatedUser = newId.Impersonate();

                    // check the identity after impersonation
                    sResult += "After impersonation: " + WindowsIdentity.GetCurrent().Name + "\r\n";

                    newUser = impersonatedUser;
                    return impersonatedUser;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close handle(s)
                if (pExistingTokenHandle != IntPtr.Zero)
                    CloseHandle(pExistingTokenHandle);
                if (pDuplicateTokenHandle != IntPtr.Zero)
                    CloseHandle(pDuplicateTokenHandle);
            }
        }

        public void UndoImpersonate()
        {
            try
            {
                if (newUser != null)
                    newUser.Undo();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to logout \n" + ex.Message);
            }
        }
    }
}


