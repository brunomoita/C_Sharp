using System;
using System.Collections.Generic;
using System.Text;

namespace assignment4.Models
{
    class Constants
    {
        public class FTP
        {
            public const string UserName = @"bdat100119f\bdat1001";
            public const string Password = "bdat1001";

            public const string BaseUrl = "ftp://waws-prod-dm1-127.ftp.azurewebsites.windows.net/bdat1001-20914";

            public const int OperationPauseTime = 10000;

            public const string myFtpFolder = "ftp://waws-prod-dm1-127.ftp.azurewebsites.windows.net/bdat1001-20914/200439932%20Bruno%20Simoes/";
        }

        public class Student
        {
            public readonly static string ExePath = Environment.CurrentDirectory;

            public readonly static string ContentFolder = $"{ExePath}\\..\\..\\..\\Content";

            public const string InfoCSVFileName = "info.csv";
            public const string MyImageFileName = "myimage.jpg";


        }
    }
}
    

