using CBF.Application.Commands;
using CBF.Application.Commands.Tournament;
using CBF.Infra.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CBF.Application.CommandHandlers.Tournament
{
    public class CreateTournamentCommandHandler : CommandHandler, IRequestHandler<CreateTournamentCommand, CommandResponse<Domain.Tournament>>
    {
        private readonly ITournamentRepository _repository;

        public CreateTournamentCommandHandler(ITournamentRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResponse<Domain.Tournament>> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            var tournament = new Domain.Tournament(request.Name, request.Reference);

            if (!tournament.IsValid)
            {
                return Fail<Domain.Tournament>(tournament.Notifications);
            }

            await _repository.Add(tournament);
            await _repository.SaveChanges();

            return Ok(tournament);
        }
    }
}
