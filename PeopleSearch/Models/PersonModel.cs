using System.ComponentModel.DataAnnotations;

namespace PeopleSearch.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Interests { get; set; }
        public string Address { get; set; }

        public string ImageLocation { get; set; }
    }
}
