using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace CBF.Application.Commands
{
    public class LoginUserCommand : Command, IRequest<CommandResponse<Domain.User>>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<Notification>()
                  .IsEmail(Email, "Email", "Email invalido")
                  .IsNotNullOrEmpty(Password, "Password", "Senha é obrigatorio"));
        }
    }
}
