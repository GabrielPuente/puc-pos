using CBF.Domain;
using CBF.Infra.Data.Auditing;
using CBF.Infra.Data.Interfaces;

namespace CBF.Infra.Data.Repositories
{
    public class TransferRepository : BaseRepository<Transfer>, ITransferRepository
    {
        public TransferRepository(DataContext ctx, IEntryAuditor entryAuditor) : base(ctx, entryAuditor)
        {
        }
    }
}