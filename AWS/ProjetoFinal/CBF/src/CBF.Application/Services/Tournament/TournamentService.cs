using CBF.Application.Commands;
using CBF.Application.Commands.Tournament;
using CBF.Application.Services.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace CBF.Application.Services.Tournament
{
    public class TournamentService : ITournamentService
    {
        private readonly IMediator _mediator;

        public TournamentService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResponse<Domain.Tournament>> CreateTournament(CreateTournamentCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResponse<Domain.Tournament>(null, command.Notifications);
            }

            var result = await _mediator.Send(command);

            return new CommandResponse<Domain.Tournament>(result.Data, result.Notifications);
        }
    }
}
