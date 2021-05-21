using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assignment_3.Models
{

    public class Student
    {

        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string _DateOfBirth;
        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set
            {
                _DateOfBirth = value;

                //Convert DateOfBirth to DateTime
                DateTime dtOut;
                DateTime.TryParse(_DateOfBirth, out dtOut);
                DateOfBirthDT = dtOut;
            }
        }

        public DateTime DateOfBirthDT { get; internal set; }
        public string ImageData { get; set; }
        public virtual int Age
        {
            get
            {
                if (DateOfBirthDT == DateTime.MinValue)
                {
                    return 0;
                }

                DateTime Now = DateTime.Now;
                int Years = new DateTime(DateTime.Now.Subtract(DateOfBirthDT).Ticks).Year - 1;
                DateTime PastYearDate = DateOfBirthDT.AddYears(Years);
                int Months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Now)
                    {
                        Months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Now)
                    {
                        Months = i - 1;
                        break;
                    }
                }
                int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
                int Hours = Now.Subtract(PastYearDate).Hours;
                int Minutes = Now.Subtract(PastYearDate).Minutes;
                int Seconds = Now.Subtract(PastYearDate).Seconds;
                if (Years > 15 & Years < 100)
                {
                    return Years;
                }
                else
                {
                    return 0;
                }                
            }
        }
        public bool MyRecord { get; set; }        
        public string AbsoluteUrl { get; set; }
        public string Directory { get; set; }
        public string FullPathUrl
        {
            get
            {
                return AbsoluteUrl + "/" + Directory;
            }
        }        

        public static int counter;

        public static string[] StudentPublicList { get; set; }

        public void FromCSV(string csvdata)
        {
            
            string[] data = csvdata.Split(",", StringSplitOptions.None);            
            var dataSize = data.Length;
            if (dataSize.Equals(5) || dataSize.Equals(6))
            {                
                StudentId = data[0];
                FirstName = data[1];
                LastName = data[2];
                DateOfBirth = data[3];
                ImageData = data[4];                
            }
            else
            {
                Console.WriteLine("CSV with incomplete data");
            }
                  
        }
        
        public void FromDirectory(string directory)
        {            
            Directory = directory;

            if (String.IsNullOrEmpty(directory.Trim()))
            {
                return;
            }

            string[] data = directory.Trim().Split(" ", StringSplitOptions.None);

            StudentId = data[0];
            FirstName = data[1];
            LastName = data[2];
        }

        public string ToCSV()
        {
            string result = $"{StudentId},{FirstName},{LastName},{DateOfBirthDT.ToShortDateString()},{MyRecord},{ImageData}";
            return result;
        }

        public override string ToString()
        {
            string result = $"{StudentId} {FirstName} {LastName} {MyRecord}";
            return result;
        }

        public string ToJSON()
        {
            StringBuilder json_data = new StringBuilder("{");
            json_data.Append($"\"StudentID\":\"{StudentId}\",");
            json_data.Append($"\"FirstName\":\"{FirstName}\",");
            json_data.Append($"\"LastName\":\"{LastName}\",");
            json_data.Append($"\"DateOfBirth\":\"{DateOfBirth}\",");
            json_data.Append($"\"ImageData\":\"{ImageData}\"");
            json_data.Append("}");

            return json_data.ToString();
        }

        public string ToXML()
        {
            StringBuilder xml_data = new StringBuilder("<Student>");
            xml_data.Append($"<StudentID>{StudentId}</StudentID>");
            xml_data.Append($"<FirstName>{FirstName}</FirstName>");
            xml_data.Append($"<LastName>{LastName}</LastName>");
            xml_data.Append($"<DateOfBirth>{DateOfBirth}</DateOfBirth>");
            xml_data.Append($"<ImageData>{ImageData}</ImageData>");
            xml_data.Append("</Student>");
            return xml_data.ToString();
        }
    }
}

