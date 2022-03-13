using CBF.Domain;
using CBF.Infra.Data.Auditing;
using CBF.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CBF.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext ctx, IEntryAuditor entryAuditor) : base(ctx, entryAuditor)
        {
            _context = ctx;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }
    }
}