using CBF.Domain.DefaultEntity;
using CBF.Infra.Data.Auditing;
using CBF.Infra.Data.Constants;
using CBF.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBF.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly DataContext _context;
        protected readonly DbSet<T> _set;
        protected readonly IEntryAuditor _entryAuditor;

        public BaseRepository(DataContext context, IEntryAuditor entryAuditor)
        {
            _context = context;
            _set = context.Set<T>();
            _entryAuditor = entryAuditor;
        }

        public virtual async Task<IEnumerable<T>> GetAll(bool trackEntities = true)
        {
            if (trackEntities)
            {
                return await _set.ToListAsync();
            }

            return await _set
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<T> Get(Guid id, bool trackEntities = true)
        {
            IQueryable<T> query = null;
            query = trackEntities ? _set : _set.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            _entryAuditor.AuditCreate(_context.Entry(entity));
            await _set.AddAsync(entity);

            return true;
        }

        public virtual Task<bool> Update(T entity)
        {
            _entryAuditor.AuditUpdate(_context.Entry(entity));
            _set.Update(entity);

            return Task.FromResult(true);
        }

        public virtual Task Delete(T entity)
        {
            var entityEntry = _context.Entry(entity);

            entityEntry.Property(Columns.IS_DELETED).CurrentValue = true;

            _entryAuditor.AuditDelete(entityEntry);

            return Task.CompletedTask;
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}
