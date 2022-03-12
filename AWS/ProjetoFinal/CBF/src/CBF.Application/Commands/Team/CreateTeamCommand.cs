using Flunt.Validations;
using MediatR;
using System;
using System.Collections.Generic;

namespace CBF.Application.Commands.Team
{
    public class CreateTeamCommand : Command, IRequest<CommandResponse<Domain.Team>>
    {
        public string Name { get; set; }

        public string Locality { get; set; }

        public IEnumerable<PlayerCommand> Players { get; set; } = new List<PlayerCommand>();

        public override void Validate()
        {
            AddNotifications(new Contract<CreateTeamCommand>()
                  .IsNotNullOrEmpty(Name, "Name", "Campo nome é obrigatorio")
                  .IsNotNullOrEmpty(Locality, "Locality", "Campo localidade é obrigatorio"));
        }
    }
}
