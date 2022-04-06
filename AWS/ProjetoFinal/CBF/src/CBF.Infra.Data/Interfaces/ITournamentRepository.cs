using CBF.Domain;
using System;
using System.Threading.Tasks;

namespace CBF.Infra.Data.Interfaces
{
    public interface ITournamentRepository : IBaseRepository<Tournament>
    {
        Task<Tournament> GetWithIncludes(Guid id);
    }
}
