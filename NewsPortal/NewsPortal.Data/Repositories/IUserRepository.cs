using NewsPortal.Data.Model;
using System.Threading.Tasks;

namespace NewsPortal.Data.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> FindUserByGoogleIdAsync(string id);

        Task<User> GetUserWithPostsAsync(int id);
    }
}
