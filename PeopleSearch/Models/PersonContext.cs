using Microsoft.EntityFrameworkCore;

namespace PeopleSearch.Models
{
    public class PersonContext :DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) 
            : base(options)
        {
        }

        public DbSet<PersonModel> People { get; set; }
    }
}
