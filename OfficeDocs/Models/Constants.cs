using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeDocs.Models
{
    class Constants
    {
        public class Sources
        {
            public const string JsonURL = "https://webapibasicsstudenttracker.azurewebsites.net/api/students";
        }

        public class Locations
        {
            public readonly static string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            public readonly static string ExePath = Environment.CurrentDirectory;

            public readonly static string ContentFolder = $"{ExePath}\\..\\..\\..\\Content";
            public const string JSONFile = "Placeholder.json";
            public readonly static string localJSON = $"{ContentFolder}\\{JSONFile}";
        }
    }
}
