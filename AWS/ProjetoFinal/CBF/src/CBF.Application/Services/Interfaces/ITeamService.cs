using CBF.Application.Commands;
using CBF.Application.Commands.Team;
using System;
using System.Threading.Tasks;
using DomainTeam = CBF.Domain.Team;

namespace CBF.Application.Services.Interfaces
{
    public interface ITeamService
    {
        Task<CommandResponse<DomainTeam>> CreateTeam(CreateTeamCommand command);

        Task<CommandResponse<DomainTeam>> AddPlayersTeam(Guid id, AddPlayersInTeamCommand command);
    }
}
