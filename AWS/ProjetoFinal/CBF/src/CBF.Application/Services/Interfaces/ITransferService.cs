using CBF.Application.Commands;
using CBF.Application.Commands.Transfer;
using System.Threading.Tasks;
using DomainTransfer = CBF.Domain.Transfer;

namespace CBF.Application.Services.Interfaces
{
    public interface ITransferService
    {
        Task<CommandResponse<DomainTransfer>> CreateTransfer(CreateTransferCommand command);
    }
}
