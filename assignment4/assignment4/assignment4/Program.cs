using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System;
using assignment4.Models;
using EO.Internal;
using System.Net;
using Assignment_3.Models.Utilities;
using System.Drawing;


//using static System.Net.Mime.MediaTypeNames;

namespace assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            //API Data
            string FullPathUrl = @"https://webapibasicsstudenttracker.azurewebsites.net/api/students";
             //string localUploadFilePath = @"C:\Users\Owner\Desktop\IES\info.docx";
             //string remoteUploadFileDestination = "/200425224 Saba Sultana/info.docx";
           

            //Download the API data
            string json = new System.Net.WebClient().DownloadString(FullPathUrl);
            List<Student> studentList = JsonConvert.DeserializeObject<List<Student>>(json);
             

            Console.WriteLine();

            //Adding Data to Document.
            string strDoc = @$"{Constants.Student.ContentFolder}"+"\\info.docx";
            //string strTxt = RO;

            //Creating Word Document
            CreateWordprocessingDocument(@$"{Constants.Student.ContentFolder}"+"\\info.docx");
            OpenAndAddTextToWordDocument(strDoc, studentList);
            //Creating Excel Document
            Models.Excel.CreateSpreadsheetWorkbook(@$"{Constants.Student.ContentFolder}"+"\\info.xlsx");
            Models.Excel.InsertText(@$"{Constants.Student.ContentFolder}"+"\\info.xlsx", studentList);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Constants.FTP.BaseUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(Constants.FTP.UserName, Constants.FTP.Password);
            string imageFilePath = Constants.FTP.myFtpFolder + "/" + Constants.Student.MyImageFileName;
            var imageBytes = Ftp.DownloadFileBytes(imageFilePath);
            Image image = Imaging.byteArrayToImage(imageBytes);
            Console.WriteLine("Downloaded image file:");
        }

        //Create a word document
        public static void CreateWordprocessingDocument(string filepath)
        {
            // Create a document by supplying the filepath. 
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new Document();
                
                Body body = mainPart.Document.AppendChild(new Body());
                ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                //using (FileStream stream = new FileStream(filepath, FileMode.Open))
                //{
                //    imagePart.FeedData(stream);
                //}
                AddImageToBody(wordDocument, mainPart.GetIdOfPart(imagePart));
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                run.AppendChild(new Text("Word Document"));
            }
        }
        public static void OpenAndAddTextToWordDocument(string filepath, List<Student> studlist)
        {
            // Open a WordprocessingDocument for editing using the filepath.
            WordprocessingDocument wordprocessingDocument =
                WordprocessingDocument.Open(filepath, true);
            //Adds new section to the document
           

            // Assign a reference to the existing document body.
            Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
            
            // Add new text.
            foreach ( var stud in studlist )
            {
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
               
                run.AppendChild(new Text($"Student ID : {stud.StudentId}"));
                run.AppendChild(new Break());
                run.AppendChild(new Text($"Student UID : {stud.StudentUID}"));
                run.AppendChild(new Break());
                run.AppendChild(new Text($"FirstName : {stud.FirstName}"));
                run.AppendChild(new Break());
                run.AppendChild(new Text($"LastName : {stud.LastName}"));
                run.AppendChild(new Break());
                run.AppendChild(new Text($"Student Code : {stud.StudentCode}"));
                run.AppendChild(new Break());
                run.AppendChild(new Text($"Date of Birth : {stud.DateOfBirth}"));
                run.AppendChild(new Break());
                run.AppendChild(new Text($"Image : {stud.Images}"));
                run.AppendChild(new Break());
                run.AppendChild(new Text($"CreateDate : {stud.CreateDate}"));
                run.AppendChild(new Break());
                run.AppendChild(new Text($"EditDate : {stud.EditDate}"));



                para = new Paragraph(new Run
                            (new Break() { Type = BreakValues.Page }));
                body.Append(para);
            }

            // Close the handle explicitly.
            wordprocessingDocument.Close();
        }
        private static void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId)
        {
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = 990000L, Cy = 792000L },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            // Append the reference to body, the element should be in a Run.
            
            wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
        }
        
    }
}
