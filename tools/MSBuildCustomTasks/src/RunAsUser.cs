using System;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuildCustomTasks
{
    public class RunAsUser : Task
    {
        public string Domain { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FileName { get; set; }

        public string Arguments { get; set; }

        public string WorkingDirectory { get; set; }

        public string SuccessErrorCode
        {
            get { return _successErrorCode; }
            set { _successErrorCode = value; }
        }
        private string _successErrorCode = "0";

        public string WaitForExit
        {
            get { return _waitForExit; }
            set { _waitForExit = value; }
        }
        private string _waitForExit = "true";

        public string ReDirectOutput
        {
            get { return _redirectOutput; }
            set { _redirectOutput = value; }
        }
        private string _redirectOutput = "true";

        public string ShowWindow
        {
            get { return _showWindow; }
            set { _showWindow = value; }
        }
        private string _showWindow = "false";

        public override bool Execute()
        {
            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    FileName = FileName,
                    Arguments = Arguments,
                    UserName = UserName,
                    Domain = Domain,
                    Password = GetSecurePassword(Password),
                    WorkingDirectory = WorkingDirectory
                }
            };
            if (ReDirectOutput == "true")
            {
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
            }
            if (ShowWindow == "false")
            {
                process.StartInfo.CreateNoWindow = true;
            }

            Log.LogMessage(MessageImportance.Normal, "Starting process '{0}' as user: {1}\\{2}", FileName, Domain, UserName);
            process.Start();
            var standardOutput = string.Empty;
            var standardError = string.Empty;
            if (ReDirectOutput == "true")
            {
                standardOutput = process.StandardOutput.ReadToEnd();
                standardError = process.StandardError.ReadToEnd();
            }
            if (WaitForExit == "true")
            {
                process.WaitForExit();
                if (process.ExitCode.ToString(CultureInfo.InvariantCulture) != SuccessErrorCode)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.Write(standardOutput);
                    System.Console.Write(standardError);
                    System.Console.ResetColor();
                    return false;
                }
                System.Console.WriteLine(standardOutput);
            }
            return true;
        }

        private static SecureString GetSecurePassword(string password)
        {
            var securePassword = new SecureString();
            foreach (var t in password)
            {
                securePassword.AppendChar(t);
            }
            return securePassword;
        }
    }
}
