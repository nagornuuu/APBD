using Exercise2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2
{
    public class Student
    {
        public string fname { get; set; }
        public string lname{ get; set; } 
        public string IndexNumber { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; } 
        public string fathersName { get; set; }
        public Studies Studies { get; set; }
    }
}
