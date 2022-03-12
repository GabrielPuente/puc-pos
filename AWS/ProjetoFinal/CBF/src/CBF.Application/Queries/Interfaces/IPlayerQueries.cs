using CBF.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBF.Application.Queries.Interfaces
{
    public interface IPlayerQueries
    {
        Task<IEnumerable<TeamPlayerViewModel>> GetAll();

        Task<TeamPlayerViewModel> GetById(Guid id);
    }
}
