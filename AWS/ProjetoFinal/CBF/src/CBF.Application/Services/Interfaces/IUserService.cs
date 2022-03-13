using CBF.Application.Commands;
using System.Threading.Tasks;
using DomainUser = CBF.Domain.User;

namespace CBF.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<CommandResponse<DomainUser>> CreateUser(CreateUserCommand command);

        Task<CommandResponse<DomainUser>> LoginUser(LoginUserCommand command);
    }
}
