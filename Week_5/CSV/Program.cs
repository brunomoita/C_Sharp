using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSV.Models;

namespace CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\bruno\Google Drive\Georgian College\Information_Encoding_Standards\info.csv";
            string fileContents;

            using (StreamReader stream = new StreamReader(filePath))
            {
                fileContents = stream.ReadToEnd();
            }
                       
            List<string> entries = new List<string>();
            entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

            string[] data = entries[1].Split(",", StringSplitOptions.None);

            Student student = new Student();
            
            student.StudentID = data[0];
            student.FirstName = data[1];
            student.LastName = data[2];
            student.DateOfBirth = data[3];
            student.ImageDtata = data[4];

            Console.WriteLine(fileContents);
        }
    }
}
