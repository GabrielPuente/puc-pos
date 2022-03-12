using CBF.Domain;
using Flunt.Validations;
using MediatR;
using System;

namespace CBF.Application.Commands.Player
{
    public class UpdatePlayerMarketValueCommand : Command, IRequest<CommandResponse<Domain.Player>>
    {
        public Guid Id { get; set; }

        public decimal MarketValue { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<UpdatePlayerMarketValueCommand>()
             .IsGreaterThan(MarketValue, 0, "MarketValue", "Valor de mercado deve ser preenchido"));
        }
    }
}
