using CBF.Application.Commands;
using CBF.Application.Commands.Tournament;
using CBF.Infra.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CBF.Application.CommandHandlers.Tournament
{
    public class AddMatchCommandHandler : CommandHandler, IRequestHandler<AddMatchCommand, CommandResponse<Domain.Match>>
    {
        private readonly ITournamentRepository _repository;
        private readonly ITeamRepository _teamRepository;

        public AddMatchCommandHandler(ITournamentRepository repository, ITeamRepository teamRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
        }

        public async Task<CommandResponse<Domain.Match>> Handle(AddMatchCommand request, CancellationToken cancellationToken)
        {
            var homeTeam = await _teamRepository.Get(request.HomeTeamId);
            var awayTeam = await _teamRepository.Get(request.AwayTeamId);
            var tournament = await _repository.Get(request.TournamentId);

            if (homeTeam is null || awayTeam is null || tournament is null)
            {
                var data = homeTeam is null ? "Time de casa" : awayTeam is null ? "Time fora de casa" : "Torneio";
                request.AddNotification(data, "Não encontrado");
                return Fail<Domain.Match>(request.Notifications);
            }
            else if(homeTeam.Id == awayTeam.Id)
            {
                request.AddNotification("Times", "Times a se enfrentares nao pode ser o mesmo");
                return Fail<Domain.Match>(request.Notifications);
            }

            var match = new Domain.Match(homeTeam, awayTeam, request.Reference);

            if (!match.IsValid)
            {
                return Fail<Domain.Match>(match.Notifications);
            }

            tournament.AddMatch(match);

            if (!tournament.IsValid)
            {
                return Fail<Domain.Match>(tournament.Notifications);
            }

            await _repository.Update(tournament);
            await _repository.SaveChanges();

            return Ok(match);
        }
    }
}
