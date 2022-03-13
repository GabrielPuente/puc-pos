using CBF.Domain;
using System.Threading.Tasks;

namespace CBF.Infra.Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
