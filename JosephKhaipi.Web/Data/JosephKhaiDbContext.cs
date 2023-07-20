using JosephKhaipi.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JosephKhaipi.Web.Data
{
    public class JosephKhaiDbContext : DbContext
    {
        public JosephKhaiDbContext(DbContextOptions<JosephKhaiDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostsLike { get; set;}
        public DbSet<BlogPostComment> BlogPostsComment { get; set;}

    }
}
