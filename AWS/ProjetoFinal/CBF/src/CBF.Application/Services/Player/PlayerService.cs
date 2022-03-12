using CBF.Application.Commands;
using CBF.Application.Commands.Player;
using CBF.Application.Services.Interfaces;
using MediatR;
using System;
using System.Threading.Tasks;
using DomainPlayer = CBF.Domain.Player;

namespace CBF.Application.Services.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IMediator _mediator;

        public PlayerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResponse<DomainPlayer>> UpdatePlayerMarketValue(Guid id, UpdatePlayerMarketValueCommand command)
        {
            command.Id = id;

            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResponse<DomainPlayer>(null, command.Notifications);
            }

            var result = await _mediator.Send(command);

            return new CommandResponse<DomainPlayer>(result.Data, result.Notifications);
        }
    }
}
