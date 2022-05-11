using Microsoft.EntityFrameworkCore;

namespace Test2.Models
{
	public class HotelsContext : DbContext
	{
		public DbSet<Hotel> Hotels { get; set; }

		public HotelsContext(DbContextOptions<HotelsContext> options)
			: base(options)
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}
	}
}
