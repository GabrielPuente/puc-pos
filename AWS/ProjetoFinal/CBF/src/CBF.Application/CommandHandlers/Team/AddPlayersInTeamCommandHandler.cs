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
    public class AddPlayersInTeamCommandHandler : CommandHandler, IRequestHandler<AddPlayersInTeamCommand, CommandResponse<DomainTeam>>
    {
        private readonly ITeamRepository _repository;

        public AddPlayersInTeamCommandHandler(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResponse<DomainTeam>> Handle(AddPlayersInTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _repository.Get(request.Id);

            if (team is null)
            {
                request.AddNotification("Id", "Time nao encontrado");
                return Fail<DomainTeam>(request.Notifications);
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

            await _repository.Update(team);
            await _repository.SaveChanges();

            return Ok(team);
        }
    }
}
