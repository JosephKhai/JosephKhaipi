using JosephKhaipi.Web.Data;
using JosephKhaipi.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JosephKhaipi.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly JosephKhaiDbContext _context;

        public BlogPostCommentRepository(JosephKhaiDbContext context)
        {
            _context = context;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment comment)
        {
            await _context.BlogPostsComment.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
            return await _context.BlogPostsComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
