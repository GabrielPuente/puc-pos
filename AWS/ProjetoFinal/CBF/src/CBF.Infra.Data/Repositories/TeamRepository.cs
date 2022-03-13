using CBF.Domain;
using CBF.Infra.Data.Auditing;
using CBF.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBF.Infra.Data.Repositories
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext ctx, IEntryAuditor entryAuditor) : base(ctx, entryAuditor)
        {
            _context = ctx;
        }

        public override async Task<IEnumerable<Team>> GetAll(bool trackEntities = true)
        {
            if (trackEntities)
            {
                return await _context.Teams
                    .Include(x => x.Players)
                    .ToListAsync();
            }

            return await _set
                .AsNoTracking()
                .Include(x => x.Players)
                .ToListAsync();
        }

        public override async Task<Team> Get(Guid id, bool trackEntities = true)
        {
            if (trackEntities)
            {
                return await _context.Teams
                    .Include(x => x.Players)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }

            return await _context.Teams
                    .AsNoTracking()
                   .Include(x => x.Players)
                   .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
