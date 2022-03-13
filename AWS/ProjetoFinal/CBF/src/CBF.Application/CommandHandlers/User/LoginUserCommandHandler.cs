using CBF.Application.Commands;
using CBF.Application.Services.Password;
using CBF.Infra.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DomainUser = CBF.Domain.User;

namespace CBF.Application.CommandHandlers.Player
{
    public class LoginUserCommandHandler : CommandHandler, IRequestHandler<LoginUserCommand, CommandResponse<DomainUser>>
    {
        private readonly IUserRepository _repository;

        public LoginUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResponse<DomainUser>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmail(request.Email);

            if(user is null)
            {
                request.AddNotification("User", "Login ou senha invalida");
                return Fail<DomainUser>(request.Notifications);
            }

            var areEqual = PasswordService.CheckPassword(request.Password, user.Password);

            if(!areEqual)
            {
                request.AddNotification("User", "Login ou senha invalida");
                return Fail<DomainUser>(request.Notifications);
            }

            return Ok(user);
        }
    }
}
