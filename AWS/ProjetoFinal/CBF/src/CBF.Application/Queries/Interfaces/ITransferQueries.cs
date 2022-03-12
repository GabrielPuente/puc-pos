using CBF.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBF.Application.Queries.Interfaces
{
    public interface ITransferQueries
    {
        Task<IEnumerable<TransferViewModel>> GetAll();

        Task<TransferViewModel> GetById(Guid id);
    }
}
