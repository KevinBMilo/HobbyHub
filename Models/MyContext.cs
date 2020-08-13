using Microsoft.EntityFrameworkCore;

namespace ExamThree.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }
	    public DbSet<User> Users {get;set;}
	    public DbSet<Passion> Passions {get;set;}
	    public DbSet<Fan> Fans {get;set;}
    }
}