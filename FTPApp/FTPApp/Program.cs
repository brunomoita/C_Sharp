using FTPApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace FTPApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Student myrecord = new Student { StudentId = "20033515", FirstName = "Chris", LastName = "Dyck" };

            List<string> directories = FTP.GetDirectory(Constants.FTP.BaseUrl);
            List<Student> students = new List<Student>();

            foreach (var directory in directories)
            {
                Student student = new Student();
                student.FromDirectory(directory);

                if (FTP.FileExists(student.InfoCSVPath))
                {
                    var csvBytes = FTP.DownloadFileBytes(student.InfoCSVPath);

                    string csvFileData = Encoding.ASCII.GetString(csvBytes, 0, csvBytes.Length);

                    string[] data = csvFileData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

                    student.FromCSV(data[1]);
                    Console.WriteLine(student.Age);
                }

                //if (FTP.FileExists(student.MyImagePath))
                //{
                //    var imageBytes = FTP.DownloadFileBytes(student.MyImagePath);
                //    Image myimage = Imaging.ByteArrayToImage(imageBytes);

                //    string base64 = Imaging.ImageToBase64(myimage, ImageFormat.Jpeg);
                //}

                students.Add(student);

                Console.WriteLine(directory);
            }

            Student me = students.SingleOrDefault(x => x.StudentId == myrecord.StudentId);
            Student meUsingFind = students.Find(x => x.StudentId == myrecord.StudentId);

            var avgage = students.Average(x => x.Age);
            var minage = students.Min(x => x.Age);
            var maxage = students.Max(x => x.Age);

            //Console.WriteLine

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }

        }
    }
}
