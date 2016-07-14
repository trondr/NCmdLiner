//Source: http://www.codeproject.com/Articles/10090/A-small-C-Class-for-impersonating-a-User

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace MSBuildCustomTasks.Common
{
    #region Using directives.
    // ----------------------------------------------------------------------

    // ----------------------------------------------------------------------
    #endregion

    /////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Impersonation of a user. Allows to execute code under another
    /// user context.
    /// Please note that the account that instantiates the Impersonator class
    /// needs to have the 'Act as part of operating system' privilege set.
    /// </summary>
    /// <remarks>	
    /// This class is based on the information in the Microsoft knowledge base
    /// article http://support.microsoft.com/default.aspx?scid=kb;en-us;Q306158
    /// 
    /// Encapsulate an instance into a using-directive like e.g.:
    /// 
    ///		...
    ///		using ( new Impersonator( "myUsername", "myDomainname", "myPassword" ) )
    ///		{
    ///			...
    ///			[code that executes under the new context]
    ///			...
    ///		}
    ///		...
    /// 
    /// Please contact the author Uwe Keim (mailto:uwe.keim@zeta-software.de)
    /// for questions regarding this class.
    /// </remarks>
    public class Impersonator : IDisposable
    {
        #region Public methods.
        // ------------------------------------------------------------------

        /// <summary>
        /// Constructor. Starts the impersonation with the given credentials.
        /// Please note that the account that instantiates the Impersonator class
        /// needs to have the 'Act as part of operating system' privilege set.
        /// </summary>
        /// <param name="userName">The name of the user to act as.</param>
        /// <param name="domainName">The domain name of the user to act as.</param>
        /// <param name="password">The password of the user to act as.</param>
        public Impersonator(
            string userName,
            string domainName,
            string password)
        {
            ImpersonateValidUser(userName, domainName, password);
        }

        // ------------------------------------------------------------------
        #endregion

        #region IDisposable member.
        // ------------------------------------------------------------------

        public void Dispose()
        {
            UndoImpersonation();
        }

        // ------------------------------------------------------------------
        #endregion

        #region P/Invoke.
        // ------------------------------------------------------------------

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int LogonUser(
            string lpszUserName,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(
            IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(
            IntPtr handle);

        private const int Logon32LogonInteractive = 2;
        private const int Logon32ProviderDefault = 0;

        // ------------------------------------------------------------------
        #endregion

        #region Private member.
        // ------------------------------------------------------------------

        /// <summary>
        /// Does the actual impersonation.
        /// </summary>
        /// <param name="userName">The name of the user to act as.</param>
        /// <param name="domain"></param>
        /// <param name="password">The password of the user to act as.</param>
        private void ImpersonateValidUser(string userName, string domain, string password)
        {
            var token = IntPtr.Zero;
            var tokenDuplicate = IntPtr.Zero;
            System.Console.WriteLine("Before impersonating windows identity: " + SecurityHelper.GetCurrentWindowsIdentityName());
            try
            {
                if (RevertToSelf())
                {
                    if (LogonUser(
                        userName,
                        domain,
                        password,
                        Logon32LogonInteractive,
                        Logon32ProviderDefault,
                        ref token) != 0)
                    {
                        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                        {
                            var tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            _impersonationContext = tempWindowsIdentity.Impersonate();
                        }
                        else
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                    else
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                if (token != IntPtr.Zero)
                {
                    CloseHandle(token);
                }
                if (tokenDuplicate != IntPtr.Zero)
                {
                    CloseHandle(tokenDuplicate);
                }
            }
            System.Console.WriteLine("After impersonating windows identity: " + SecurityHelper.GetCurrentWindowsIdentityName());
        }

        /// <summary>
        /// Reverts the impersonation.
        /// </summary>
        private void UndoImpersonation()
        {
            if (_impersonationContext != null)
            {
                _impersonationContext.Undo();
            }
        }

        private WindowsImpersonationContext _impersonationContext;

        // ------------------------------------------------------------------
        #endregion
    }

    /////////////////////////////////////////////////////////////////////////
}