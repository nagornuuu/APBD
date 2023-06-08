using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exercise2.Classes
{
    public class ActiveStudies
    {
        public string name { get; set; }    
        public string numberOfStudents { get; set; }
        public override bool Equals(object obj)
        {
            return obj is ActiveStudies subject &&
                   name == subject.name;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        }
    }
}
