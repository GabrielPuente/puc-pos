using CBF.Application.Commands;
using CBF.Application.Commands.Team;
using CBF.Application.Services.Interfaces;
using MediatR;
using System;
using System.Threading.Tasks;
using DomainTeam = CBF.Domain.Team;

namespace CBF.Application.Services.Team
{
    public class TeamService : ITeamService
    {
        private readonly IMediator _mediator;

        public TeamService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResponse<DomainTeam>> CreateTeam(CreateTeamCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResponse<DomainTeam>(null, command.Notifications);
            }

            var result = await _mediator.Send(command);

            return new CommandResponse<DomainTeam>(result.Data, result.Notifications);
        }

        public async Task<CommandResponse<DomainTeam>> AddPlayersTeam(Guid id, AddPlayersInTeamCommand command)
        {
            command.Id = id;

            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResponse<DomainTeam>(null, command.Notifications);
            }

            var result = await _mediator.Send(command);

            return new CommandResponse<DomainTeam>(result.Data, result.Notifications);
        }
    }
}
