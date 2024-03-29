﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CSV.Models.Utilities
{
    public class FTP
    {
        public static List<string> GetDirectory(string url, string username = Constants.FTP.Username, string password = Constants.FTP.Password)
        {
            List<string> output = new List<string>();

            //Build the WebRequest
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential(username, password);

            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.EnableSsl = false;

            //Connect to the FTP server and prepare a Response
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                //Get a reference to the Response stream
                using (Stream responseStream = response.GetResponseStream())
                {
                    //Read the Response stream
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        //Retrieve the entire contents of the Response
                        string responseString = reader.ReadToEnd();

                        //Split the response by Carriage Return and Line Feed character to return a list of directories
                        output = responseString.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                }

                Console.WriteLine($"Directory List Complete with status code: {response.StatusDescription}");
            }

            return (output);
        }
    }

    /// <summary>
    /// Tests to determine whether a file exists on an FTP site
    /// </summary>
    /// <param name="remoteFileUrl"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static bool FileExists(string remoteFileUrl, string username = Constants.FTP.Username, string password = Constants.FTP.Password)
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

}
