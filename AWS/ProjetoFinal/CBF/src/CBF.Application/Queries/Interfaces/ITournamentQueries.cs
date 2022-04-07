using CBF.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBF.Application.Queries.Interfaces
{
    public interface ITournamentQueries
    {
        Task<IEnumerable<TournamentViewModel>> GetAll();

        Task<TournamentViewModel> GetAllMatchByTournamentId(Guid id);

        Task<TournamentViewModel> GetAllEventMatchByTournamentId(Guid id, Guid matchId);
    }
}
