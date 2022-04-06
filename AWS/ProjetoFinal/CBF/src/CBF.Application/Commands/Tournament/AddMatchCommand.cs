using Flunt.Validations;
using MediatR;
using System;

namespace CBF.Application.Commands.Tournament
{
    public class AddMatchCommand : Command, IRequest<CommandResponse<Domain.Match>>
    {
        public Guid HomeTeamId { get; set; }

        public Guid AwayTeamId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid TournamentId { get; set; }

        public DateTime Reference { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<AddMatchCommand>()
                 .IsNotEmpty(HomeTeamId, "HomeTeamId", "Time de casa deve ser informado")
                  .IsNotEmpty(AwayTeamId, "AwayTeamId", "Time fora de casa deve ser informado")
                  .IsNotEmpty(TournamentId, "TournamentId", "Torneio deve ser informado")
                  .IsGreaterThan(Reference, DateTime.MinValue, "Reference", "Data de reference deve ser informado"));
        }
    }
}
