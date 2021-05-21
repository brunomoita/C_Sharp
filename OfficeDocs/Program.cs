using System;
using System.IO;
using System.Net;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeDocs.Models;

namespace OfficeDocs
{
    class Program
    {
        static void Main(string[] args)
        {
            using var client = new WebClient();
            client.Headers.Add("User-Agent", "C# Weather API");
            client.Headers.Add("Accept", "application/json");
            client.Headers.Add("Content-Type", "application/json");

            string jsonGetData = client.DownloadString(Constants.Sources.JsonURL);
            
            var jsonResult = JsonConvert.DeserializeObject<jsonStudents>(jsonGetData);
            Console.WriteLine(jsonResult);

        }
    }
}
