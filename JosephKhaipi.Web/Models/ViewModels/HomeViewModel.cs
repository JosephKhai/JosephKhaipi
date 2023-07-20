using JosephKhaipi.Web.Models.Domain;
using System.Collections;

namespace JosephKhaipi.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
