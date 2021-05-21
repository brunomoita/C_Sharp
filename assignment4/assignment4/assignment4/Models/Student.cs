using System;
using System.Collections.Generic;
using System.Text;

namespace assignment4
{
    class Student
    {
            public int StudentId { get; set; }
            public string StudentUID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string StudentCode { get; set; }
            public DateTime DateOfBirth { get; set; }
            public List<object> Images { get; set; }
            public DateTime CreateDate { get; set; }
            public DateTime EditDate { get; set; }
            public int age { get; set; }
            public string StudentGUID { get; set; }
        public bool Isme { get; set; }
        }
}
