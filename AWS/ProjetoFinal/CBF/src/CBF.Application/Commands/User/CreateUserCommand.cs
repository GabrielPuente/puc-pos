using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;

namespace CBF.Application.Commands
{
    public class CreateUserCommand : Command, IRequest<CommandResponse<Domain.User>>
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime BirthDate { get; set; }

        public string Document { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<Notification>()
                  .IsNotNullOrEmpty(Name, "Name", "Campo nome é obrigatorio")
                  .IsEmail(Email, "Email", "Email invalido")
                  .IsCpf(Document, "Document", "Documento invalido")
                  .IsNotNullOrEmpty(Password, "Password", "Senha é obrigatorio")
                  .IsNotNullOrEmpty(Role, "Role", "Campo cargo é obrigatorio")
                  .IsGreaterThan(BirthDate, DateTime.MinValue, "BirthDate", "Campo data de nascimento invalida")
                  .IsNotNullOrEmpty(Country, "Country", "Campo localidade é obrigatorio"));
        }
    }
}
