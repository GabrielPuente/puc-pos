using Flunt.Validations;
using MediatR;
using System;

namespace CBF.Application.Commands.Match
{
    public class CreateMatchCommand : Command, IRequest<CommandResponse<Domain.Match>>
    {
        public Guid HomeTeamId { get; set; }

        public Guid AwayTeamId { get; set; }

        public Guid TournamentId { get; set; }

        public DateTime Reference { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<CreateMatchCommand>()
                 .IsNotEmpty(HomeTeamId, "HomeTeamId", "Time de casa deve ser informado")
                  .IsNotEmpty(AwayTeamId, "AwayTeamId", "Time fora de casa deve ser informado")
                  .IsNotEmpty(TournamentId, "TournamentId", "Torneio deve ser informado")
                  .IsGreaterThan(Reference, DateTime.MinValue, "Reference", "Data de reference deve ser informado"));
        }
    }
}
