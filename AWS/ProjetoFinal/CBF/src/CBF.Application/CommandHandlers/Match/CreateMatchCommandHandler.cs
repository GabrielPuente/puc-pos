using CBF.Application.Commands;
using CBF.Application.Commands.Match;
using CBF.Infra.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CBF.Application.CommandHandlers.Match
{
    public class CreateMatchCommandHandler : CommandHandler, IRequestHandler<CreateMatchCommand, CommandResponse<Domain.Match>>
    {
        private readonly IMatchRepository _repository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public CreateMatchCommandHandler(IMatchRepository repository, ITeamRepository teamRepository, ITournamentRepository tournamentRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<CommandResponse<Domain.Match>> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var homeTeam = await _teamRepository.Get(request.HomeTeamId);
            var awayTeam = await _teamRepository.Get(request.AwayTeamId);
            var tournament = await _tournamentRepository.Get(request.TournamentId);

            if (homeTeam is null || awayTeam is null || tournament is null)
            {
                var data = homeTeam is null ? "Time de casa" : awayTeam is null ? "Time fora de casa" : "Torneio";
                request.AddNotification(data, "Não encontrado");
                return Fail<Domain.Match>(request.Notifications);
            }

            var transfer = new Domain.Match(tournament, homeTeam, awayTeam, request.Reference);

            if (!transfer.IsValid)
            {
                return Fail<Domain.Match>(transfer.Notifications);
            }

            await _repository.Add(transfer);
            await _repository.SaveChanges();

            return Ok(transfer);
        }
    }
}
