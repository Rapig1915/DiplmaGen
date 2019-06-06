using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    public class StudentInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Degree { get; set; }
        public string ProgramName { get; set; }
    }
}