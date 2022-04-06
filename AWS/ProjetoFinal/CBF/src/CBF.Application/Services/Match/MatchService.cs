using CBF.Application.Commands;
using CBF.Application.Commands.Match;
using CBF.Application.Services.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace CBF.Application.Services.Match
{
    public class MatchService : IMatchService
    {
        private readonly IMediator _mediator;

        public MatchService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResponse<Domain.Match>> CreateMatch(CreateMatchCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResponse<Domain.Match>(null, command.Notifications);
            }

            var result = await _mediator.Send(command);

            return new CommandResponse<Domain.Match>(result.Data, result.Notifications);
        }
    }
}
