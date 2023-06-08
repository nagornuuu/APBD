using Exercise2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exercise2
{
    public class Uczelnia
    {
        public string CreatedAt { get; set; }
        public string Author { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public List<ActiveStudies> ActiveStudies { get; set; }
    }
}
