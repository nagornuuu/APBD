using System.ComponentModel.DataAnnotations;

namespace Czwiczenia11.Data
{
    public class Student
    {
        public int ID { get; set; }
        public string Avatar { get; set; } = null!;
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;
        
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Studies { get; set; } = null;
    }
}
