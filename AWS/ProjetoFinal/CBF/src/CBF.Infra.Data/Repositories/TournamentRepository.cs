using CBF.Domain;
using CBF.Infra.Data.Auditing;
using CBF.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CBF.Infra.Data.Repositories
{
    public class TournamentRepository : BaseRepository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(DataContext ctx, IEntryAuditor entryAuditor) : base(ctx, entryAuditor)
        {
        }

        public async Task<Tournament> GetWithIncludes(Guid id)
        {
            return await _set
                .Include(x => x.Matches)
                    .ThenInclude(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}