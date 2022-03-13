using CBF.Application.Commands;
using CBF.Application.Services.Interfaces;
using MediatR;
using System.Threading.Tasks;
using DomainUser = CBF.Domain.User;

namespace CBF.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResponse<DomainUser>> CreateUser(CreateUserCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResponse<DomainUser>(null, command.Notifications);
            }

            var result = await _mediator.Send(command);

            return new CommandResponse<DomainUser>(result.Data, result.Notifications);
        }

        public async Task<CommandResponse<DomainUser>> LoginUser(LoginUserCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResponse<DomainUser>(null, command.Notifications);
            }

            var result = await _mediator.Send(command);

            return new CommandResponse<DomainUser>(result.Data, result.Notifications);
        }
    }
}
