using CBF.Application.Commands;
using CBF.Application.Commands.Match;
using System.Threading.Tasks;

namespace CBF.Application.Services.Interfaces
{
    public interface IMatchService
    {
        Task<CommandResponse<Domain.Match>> CreateMatch(CreateMatchCommand command);
    }
}
