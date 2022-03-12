using Flunt.Validations;
using MediatR;
using System;

namespace CBF.Application.Commands.Transfer
{
    public class CreateTransferCommand : Command, IRequest<CommandResponse<Domain.Transfer>>
    {
        public Guid TeamOriginId { get; set; }

        public Guid TeamDestinyId { get; set; }

        public Guid PlayerId { get; set; }

        public decimal Value { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<CreateTransferCommand>()
                  .IsNotEmpty(TeamOriginId, "TeamOriginId", "Time de origem deve ser informado")
                  .IsNotEmpty(TeamDestinyId, "TeamDestinyId", "Time de destino deve ser informado")
                  .IsNotEmpty(PlayerId, "PlayerId", "Jogador deve ser informado")
                  .IsGreaterThan(Value, 0, "Value", "Campo valor deve ser maior que zero"));
        }
    }
}
