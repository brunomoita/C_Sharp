﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace assignment4.Models
{
    class Ftp
    {

 
        /// <summary>
        /// Uploads a file to an FTP site
        /// </summary>
        /// <param name="sourceFilePath">Local file</param>
        /// <param name="destinationFileUrl">Destination Url</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string UploadFile(string sourceFilePath, string destinationFileUrl, string username = Constants.FTP.UserName, string password = Constants.FTP.Password)
        {
            string output;

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(destinationFileUrl);

            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(username, password);

            // Copy the contents of the file to the request stream.
            byte[] fileContents = GetStreamBytes(sourceFilePath);

            //Get the length or size of the file
            request.ContentLength = fileContents.Length;

            //Write the file to the stream on the server
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            //Send the request
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                output = $"Upload File Complete, status {response.StatusDescription}";
            }
            Thread.Sleep(Constants.FTP.OperationPauseTime);

            return (output);
        }

        private static byte[] GetStreamBytes(string sourceFilePath)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Tests to determine whether a file exists on an FTP site
        /// </summary>
        /// <param name="remoteFileUrl"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool FileExists(string remoteFileUrl, string username = Constants.FTP.UserName, string password = Constants.FTP.Password)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteFileUrl);

            //Specify the method of transaction
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(username, password);

            try
            {
                //Create an instance of a Response object
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    //Does not exist
                    return false;
                }
            }

            return true;
        }

        public static byte[] DownloadFileBytes(string sourceFileUrl, string username = Constants.FTP.UserName, string password = Constants.FTP.Password)
        {
            byte[] output;

            try
            {
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(sourceFileUrl);

                //Specify the method of transaction
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(username, password);

                //Create an instance of a Response object
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    //Request a Response from the server
                    output = ToByteArray(response.GetResponseStream());

                    //Thread.Sleep(Constants.FTP.OperationPauseTime);

                    //Return the output of the Response
                    return output;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new byte[0];
        }

        public static byte[] ToByteArray(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] chunk = new byte[1024];
                int bytesRead;
                while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
                {
                    ms.Write(chunk, 0, bytesRead);
                }

                return ms.ToArray();
            }
        }

    }
}
