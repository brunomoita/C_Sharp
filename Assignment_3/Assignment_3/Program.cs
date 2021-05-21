using Assignment_3.Models;
using Assignment_3.Models.Utilities;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using CSVReader.Models;
using System.Drawing;

namespace Assignment_3
{
    class Program
    {
        static void Main(string[] args)
        {

            {
                CsvFileReaderWriter reader = new CsvFileReaderWriter();
                List<string> directories = FTP.GetDirectory(Constants.FTP.BaseUrl);
                List<Student> students = new List<Student>();

                foreach (var directory in directories)
                {
                    Student student = new Student() { AbsoluteUrl = Constants.FTP.BaseUrl };
                    student.FromDirectory(directory);

                    Student.counter++;
                    Console.WriteLine("Position " + students.Count);

                    Console.WriteLine(student);
                    string infoFilePath = student.FullPathUrl + "/" + Constants.Locations.InfoFile;

                    bool fileExists = FTP.FileExists(infoFilePath);
                    if (fileExists == true)
                    {
                        Console.WriteLine("Found info file:");
                        var infoFileBytes = FTP.DownloadFileBytes(infoFilePath);
                        //Convert from infoFileBytes incoming bytes to string
                        string asciiString = Encoding.ASCII.GetString(infoFileBytes, 0, infoFileBytes.Length);
                        var csvFile = reader.ParseString(asciiString);
                        //var entries = reader.GetEntities(csvFile);     


                        if (csvFile.Count.Equals(2))
                        {
                            student.FromCSV(csvFile[1]);

                            //if (student.StudentId.Equals(Constants.myrecord.StudentId))
                            //{
                            //    student.MyRecord = true;
                            //}
                            if (student.Age >= 15 && student.Age < 100)
                            {
                                Console.WriteLine("The student age is " + student.Age);
                            }
                            else
                            {
                                Console.WriteLine("Invalid age detected");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Bad CSV data detected");
                        }
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
                        var imageBytes = FTP.DownloadFileBytes(imageFilePath);
                        Image image = Imaging.byteArrayToImage(imageBytes);
                        //student.ImageData = Imaging.ImageToBase64(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        Console.WriteLine("Could not find image file:");
                    }

                    Console.WriteLine("\t" + imageFilePath);



                    students.Add(student);
                }
                Student me = students.SingleOrDefault(x => x.StudentId.Equals(Constants.myrecord.StudentId));
                me.MyRecord = true;

                var avgage = students.Average(x => x.Age);
                var minage = students.Min(x => x.Age);
                var maxage = students.Max(x => x.Age);
                Console.WriteLine("Thera are a total of " + students.Count + " records.");
                Console.WriteLine("The students age average is " + avgage);
                Console.WriteLine("The older student  is " + maxage + " years");
                Console.WriteLine("The younger student  is " + minage + " years");

                foreach (var student in students)
                {
                    Console.WriteLine(student.ToString());
                    //Console.WriteLine(student.ToCSV());
                }

                using (var file = File.CreateText($@"{Constants.Locations.ContentFolder}"+"\\Records.csv"))
                {
                    file.WriteLine("StudentId,FirstName,LastName,DateOfBirth,MyRecord,ImageData");
                    foreach (var student in students)
                    {                        
                        file.WriteLine(string.Join(",", student.ToCSV()));                        
                    }
                }

                using (var file = File.CreateText($@"{Constants.Locations.ContentFolder}"+"\\Records.xml"))
                {
                    foreach (var student in students)
                    {
                        file.WriteLine(string.Join(",", student.ToXML()));                        
                    }
                }

                using (var file = File.CreateText($@"{Constants.Locations.ContentFolder}" + "\\Records.json"))
                {
                    foreach (var student in students)
                    {
                        file.WriteLine(string.Join(",", student.ToJSON()));                        
                    }
                }
                UploadFolder(Constants.Locations.ContentFolder, Constants.FTP.BaseUrl);
            }
        }

        private static void UploadFolder(string source, string uploadpath)
        {
            WebRequest request = WebRequest.Create(uploadpath);
            request.Credentials = new NetworkCredential(Constants.FTP.Username, Constants.FTP.Password);
            string[] files = Directory.GetFiles(source, "*.*");
            var myfolder = Constants.FTP.myFtpFolder;
            FtpWebRequest request2 = (FtpWebRequest)WebRequest.Create(myfolder);
            //string[] subFolders = Directory.GetDirectories(source);


            foreach (string file in files)
            {
                request = WebRequest.Create(file);
                request.Method = WebRequestMethods.Ftp.UploadFile;
            }
            //foreach (string subFolder in subFolders)
            //{
            //    request = WebRequest.Create(uploadpath + "/" + Path.GetFileName(subFolder));
            //    request.Method = WebRequestMethods.Ftp.MakeDirectory; < br />
            //    request.Credentials = new NetworkCredential(userName, password);
            //    uploadFolder(subFolder, uploadpath + "/" + Path.GetFileName(subFolder));
            //}
        }
    }
}


