using Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Context
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonPicture> PeoplePicture { get; set; }
        public virtual DbSet<PersonType> PeopleType { get; set; }
    }
    
}
