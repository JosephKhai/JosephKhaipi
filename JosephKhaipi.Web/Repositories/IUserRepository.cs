using Microsoft.AspNetCore.Identity;

namespace JosephKhaipi.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();

    }
}
