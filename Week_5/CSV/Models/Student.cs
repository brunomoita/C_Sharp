using System;
using System.Collections.Generic;
using System.Text;

namespace CSV.Models
{
    public class Student
    {
        public string StudentID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        private string _DateOfBirth;

        public string DateOfBirth
            {
                get { return _DateOfBirth; }
                set { _DateOfBirth = value;

                    //Convert DateOfBirth to DateTime
                    DateTime dtOut;
                    DateTime.TryParse(_DateOfBirth, out dtOut);
                    DateOfBirthDT = dtOut;
                    }
        

            }
        public string ImageData { get; set; }

        public void FromCSV (string csvdata)
        {
            string[] data = csvdata.Split(",", StringSplitOptions.None);
            StudentID = data[0];
            FirstName = data[1];
            LastName = data[2];
            DateOfBirth = data[3];
            ImageData = data[4];
        }
        
        public string ToCSV()
        {
            string result = $"{StudentID},{FirstName},{LastName},{DateOfBirth},{ImageData}";
            return result;
        }

        public override string ToString()
        {
            string result = $"{StudentID},{FirstName},{LastName}";
            return result;
        }
    }
}
