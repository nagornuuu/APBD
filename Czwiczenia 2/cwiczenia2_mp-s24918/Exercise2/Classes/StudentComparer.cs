using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2
{
    public class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student? x, Student? y)
        {
            if (x == null || y == null) return false;
            return (x.fname == y.fname)
                && (x.lname == y.lname)
                && (x.IndexNumber == y.IndexNumber);
        }
        public int GetHashCode([DisallowNull] Student obj)
        {
            return obj.IndexNumber.GetHashCode();
        }
    }
}
