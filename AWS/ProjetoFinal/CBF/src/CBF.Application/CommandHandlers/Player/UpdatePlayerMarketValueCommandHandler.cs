using CBF.Application.Commands;
using CBF.Application.Commands.Player;
using CBF.Infra.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DomainPlayer = CBF.Domain.Player;

namespace CBF.Application.CommandHandlers.Player
{
    public class UpdatePlayerMarketValueCommandHandler : CommandHandler, IRequestHandler<UpdatePlayerMarketValueCommand, CommandResponse<DomainPlayer>>
    {
        private readonly IPlayerRepository _repository;

        public UpdatePlayerMarketValueCommandHandler(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResponse<DomainPlayer>> Handle(UpdatePlayerMarketValueCommand request, CancellationToken cancellationToken)
        {
            var player = await _repository.Get(request.Id);

            player.ChangeMarketValue(request.MarketValue);
            _repository.Update(player);
            await _repository.SaveChanges();

            return Ok(player);
        }
    }
}
