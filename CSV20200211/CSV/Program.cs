using CSV.Models;
using CSV.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> directories = FTP.GetDirectory(Constants.FTP.BaseUrl);

            foreach(var directory in directories)
            {
                Student student = new Student() { AbsoluteUrl = Constants.FTP.BaseUrl };
                student.FromDirectory(directory);

                Console.WriteLine(student);
                string infoFilePath = student.FullPathUrl + "/" + Constants.Locations.InfoFile;

                bool fileExists = FTP.FileExists(infoFilePath);
                if(fileExists == true)
                {
                    Console.WriteLine("Found info file:");
                }
                else
                {
                    Console.WriteLine("Could not find info file:");
                }

                Console.WriteLine("\t" + infoFilePath);

                string imageFilePath = student.FullPathUrl + "/" + Constants.Locations.ImageFile;

                bool imageFileExists = FTP.FileExists(imageFilePath);

                if (imageFileExists == true)
                {
                    Console.WriteLine("Found image file:");
                }
                else
                {
                    Console.WriteLine("Could not find image file:");
                }

                Console.WriteLine("\t" + imageFilePath);

                //Console.WriteLine(directory);
            }


            return;

            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            //string exePath = Environment.CurrentDirectory;
            //string dataFolder = $"{exePath}\\..\\..\\..\\Content\\Data";
            //string imagesFolder = $"{exePath}\\..\\..\\..\\Content\\Images";

            //string filePath = $@"{Constants.Locations.DataFolder}\{Constants.Locations.InfoFile}";
            //string fileContents;

            //using (StreamReader stream = new StreamReader(filePath))
            //{
            //    fileContents = stream.ReadToEnd();
            //}

            //List<string> entries = new List<string>();

            //entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

            //Student student = new Student();
            //student.FromCSV(entries[1]);



            //string[] data = entries[1].Split(",", StringSplitOptions.None);

            //Student student = new Student();
            //student.StudentId = data[0];
            //student.FirstName = data[1];
            //student.LastName = data[2];
            //student.DateOfBirth = data[3];
            //student.ImageData = data[4];

            //Console.WriteLine(student.ToCSV());
            //Console.WriteLine(student.ToString());



            //string imagefilePath = $"{Constants.Locations.ImagesFolder}\\{Constants.Locations.ImageFile}";
            //Image image = Image.FromFile(imagefilePath);
            //string base64Image = Imaging.ImageToBase64(image, ImageFormat.Jpeg);
            //student.ImageData = base64Image;

            //string newfilePath = $"{Constants.Locations.DesktopPath}\\{student.ToString()}.jpg";
            //FileInfo newfileinfo = new FileInfo(newfilePath);
            //Image studentImage = Imaging.Base64ToImage(student.ImageData);
            //studentImage.Save(newfileinfo.FullName, ImageFormat.Jpeg);
        }

    }
}
