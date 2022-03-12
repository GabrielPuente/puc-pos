using Flunt.Validations;
using MediatR;
using System;
using System.Collections.Generic;

namespace CBF.Application.Commands.Team
{
    public class AddPlayersInTeamCommand : Command, IRequest<CommandResponse<Domain.Team>>
    {
        public Guid Id { get; set; }

        public IEnumerable<PlayerCommand> Players { get; set; }

        public override void Validate()
        {
            foreach (var player in Players)
            {
                AddNotifications(new Contract<AddPlayersInTeamCommand>()
                 .IsNotNullOrEmpty(player.Name, "Name", "Campo nome é obrigatorio")
                 .IsGreaterThan(player.BirthDate, DateTime.MinValue, "BirthDate", "Campo data de nascimento invalida")
                 .IsNotNullOrEmpty(player.Country, "Country", "Campo localidade é obrigatorio"));
            }

            AddNotifications(new Contract<AddPlayersInTeamCommand>()
                  .IsNotEmpty(Id, "TeamId", "Obrigatorio associar a um time"));
        }
    }
}
