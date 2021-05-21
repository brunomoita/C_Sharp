using Assignment_3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_3.Models
{
    class Constants
    {
        public static Student myrecord = new Student { StudentId = "200439932", FirstName = "Bruno", LastName = "Simoes" };
        public class Locations
        {
            public readonly static string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            public readonly static string ExePath = Environment.CurrentDirectory;

            public readonly static string ContentFolder = $"{ExePath}\\..\\..\\..\\Content";
            //public readonly static string DataFolder = $"{ContentFolder}\\Data";
            //public readonly static string ImagesFolder = $"{ContentFolder}\\Images";

            public const string InfoFile = "info.csv";
            public const string ImageFile = "myimage.jpg";

            public const string csvRecords = "Records.csv";
            public const string xmlRecords = "Records.xml";
            public const string jsonRecords = "Records.json";

            public readonly static string localInfo = $"{ContentFolder}\\{InfoFile}";
            public readonly static string localImage = $"{ContentFolder}\\{ImageFile}";
        }
        public class FTP
        {
            public const string Username = @"bdat100119f\bdat1001";
            public const string Password = "bdat1001";

            public const string BaseUrl = "ftp://waws-prod-dm1-127.ftp.azurewebsites.windows.net/bdat1001-20914";

            public const int OperationPauseTime = 10000;

            public const string myFtpFolder = "ftp://waws-prod-dm1-127.ftp.azurewebsites.windows.net/bdat1001-20914/200439932%20Bruno%20Simoes/";
        }

        
    }
}
