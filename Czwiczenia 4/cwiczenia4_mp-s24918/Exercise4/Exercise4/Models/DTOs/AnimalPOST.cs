using System.ComponentModel.DataAnnotations;

namespace Exercise4.Models.DTOs
{
    public class AnimalPOST
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public string? Area { get; set; }
    }
}
