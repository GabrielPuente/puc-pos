using CBF.Infra.Data.Constants;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace CBF.Infra.Data.Auditing
{
    public class EntryAuditor : IEntryAuditor
    {
        public void AuditCreate(EntityEntry entry, DateTime? date = null)
        {
            entry.Property(Columns.CREATED_DATE).CurrentValue = GetValueOrDefaultDate(date);
        }

        public void AuditUpdate(EntityEntry entry, DateTime? date = null)
        {
            entry.Property(Columns.LAST_UPDATED_DATE).CurrentValue = GetValueOrDefaultDate(date);
        }

        public void AuditDelete(EntityEntry entry, DateTime? date = null)
        {
            entry.Property(Columns.DELETED_DATE).CurrentValue = GetValueOrDefaultDate(date);
        }

        private static DateTime GetValueOrDefaultDate(DateTime? date)
        {
            return date ?? DateTime.UtcNow;
        }
    }
}
