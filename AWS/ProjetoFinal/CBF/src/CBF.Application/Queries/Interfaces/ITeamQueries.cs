using CBF.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBF.Application.Queries.Interfaces
{
    public interface ITeamQueries
    {
        Task<IEnumerable<TeamViewModel>> GetAll();

        Task<TeamViewModel> GetById(Guid id);
    }
}
