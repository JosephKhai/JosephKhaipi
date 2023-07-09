using JosephKhaipi.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JosephKhaipi.Web.Data
{
    public class JosephKhaiDbContext : DbContext
    {
        public JosephKhaiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
