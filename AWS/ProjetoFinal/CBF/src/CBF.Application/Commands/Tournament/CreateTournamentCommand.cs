using Flunt.Validations;
using MediatR;
using System;

namespace CBF.Application.Commands.Tournament
{
    public class CreateTournamentCommand : Command, IRequest<CommandResponse<Domain.Tournament>>
    {
        public string Name { get; set; }

        public DateTime Reference { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<CreateTournamentCommand>()
                  .IsNotNullOrEmpty(Name, "Name", "Nome deve ser informado")
                  .IsGreaterThan(Reference, DateTime.MinValue, "Reference", "Data de reference deve ser informado"));
        }
    }
}
