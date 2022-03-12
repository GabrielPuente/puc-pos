using CBF.Application.Commands;
using CBF.Application.Commands.Player;
using System;
using System.Threading.Tasks;
using DomainPlayer = CBF.Domain.Player;

namespace CBF.Application.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<CommandResponse<DomainPlayer>> UpdatePlayerMarketValue(Guid id, UpdatePlayerMarketValueCommand command);
    }
}
