using CBF.Application.Commands;
using CBF.Application.Commands.Transfer;
using CBF.Infra.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DomainTransfer = CBF.Domain.Transfer;

namespace CBF.Application.CommandHandlers.Team
{
    public class CreateTransferCommandHandler : CommandHandler, IRequestHandler<CreateTransferCommand, CommandResponse<DomainTransfer>>
    {
        private readonly ITransferRepository _repository;
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerRepository _playerRepository;

        public CreateTransferCommandHandler(ITransferRepository repository, ITeamRepository teamRepository, IPlayerRepository playerRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        }

        public async Task<CommandResponse<DomainTransfer>> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            var teamOrigin = await _teamRepository.Get(request.TeamOriginId);
            var teamDestiny = await _teamRepository.Get(request.TeamDestinyId);
            var player = await _playerRepository.Get(request.PlayerId);

            if (teamOrigin is null || teamDestiny is null || player is null)
            {
                var data = teamOrigin is null ? "Time origem" : teamDestiny is null ? "Time destino" : "Jogador";
                request.AddNotification(data, "Não encontrado");
                return Fail<DomainTransfer>(request.Notifications);
            }

            var transfer = new DomainTransfer(teamOrigin, teamDestiny, player, request.Value);

            if (!transfer.IsValid)
            {
                return Fail<DomainTransfer>(transfer.Notifications);
            }

            player.ChangeTeam(teamDestiny.Id);
            teamOrigin.DeletePlayer(player);
            teamDestiny.AddPlayer(player);

            await _repository.Add(transfer);
            await _repository.SaveChanges();

            return Ok(transfer);
        }
    }
}
