using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIMet.Models
{
    public class CoursesContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public CoursesContext(DbContextOptions<CoursesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
