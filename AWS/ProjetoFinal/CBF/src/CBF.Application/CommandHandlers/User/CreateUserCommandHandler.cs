using CBF.Application.Commands;
using CBF.Application.Services.Password;
using CBF.Infra.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DomainUser = CBF.Domain.User;

namespace CBF.Application.CommandHandlers.Player
{
    public class CreateUserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand, CommandResponse<DomainUser>>
    {
        private readonly IUserRepository _repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResponse<DomainUser>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var password = PasswordService.Encrypt(request.Password);

           var user = new DomainUser(request.Name, request.Country, request.BirthDate, request.Document,request.Email, password, request.Role);

            if (!user.IsValid)
            {
                return Fail<DomainUser>(user.Notifications);
            }

            await _repository.Add(user);
            await _repository.SaveChanges();

            return Ok(user);
        }
    }
}
