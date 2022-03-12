using CBF.Domain;
using CBF.Infra.Data.Auditing;
using CBF.Infra.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace CBF.Infra.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        protected readonly DataContext _context;
        protected readonly IEntryAuditor _entryAuditor;

        public PlayerRepository(DataContext context, IEntryAuditor entryAuditor)
        {
            _context = context;
            _entryAuditor = entryAuditor;
        }

        public async Task<Player> Get(Guid id)
        {
            return await _context.Players.FindAsync(id);
        }

        public void Update(Player player)
        {
            _context.Players.Update(player);
            return;
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}