using EnovationAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace EnovationAssignment.Persistance
{
	public class AppDbContext : DbContext
	{
		public DbSet<Person> People { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
	}
}
