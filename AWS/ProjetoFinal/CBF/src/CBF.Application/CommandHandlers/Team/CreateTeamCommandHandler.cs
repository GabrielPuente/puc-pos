using CBF.Application.Commands;
using CBF.Application.Commands.Team;
using CBF.Infra.Data.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DomainTeam = CBF.Domain.Team;
using DomainPlayer = CBF.Domain.Player;

namespace CBF.Application.CommandHandlers.Team
{
    public class CreateTeamCommandHandler : CommandHandler, IRequestHandler<CreateTeamCommand, CommandResponse<DomainTeam>>
    {
        private readonly ITeamRepository _repository;

        public CreateTeamCommandHandler(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResponse<DomainTeam>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new DomainTeam(request.Name, request.Locality);

            if (!team.IsValid)
            {
                return Fail<DomainTeam>(team.Notifications);
            }

            foreach (var player in request.Players)
            {
                team.AddPlayer(
                    new DomainPlayer(
                        team.Id,
                        player.Name,
                        player.BirthDate,
                        player.Country,
                        player.MarketValue));
            }

            if (team.Players.Any())
            {
                team.CheckDomainPlayersIsValid();
            }

            if (!team.IsValid)
            {
                return Fail<DomainTeam>(team.Notifications);
            }

            await _repository.Add(team);
            await _repository.SaveChanges();

            return Ok(team);
        }
    }
}
