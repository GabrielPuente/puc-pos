using CBF.Application.Commands;
using CBF.Application.Commands.Tournament;
using System.Threading.Tasks;

namespace CBF.Application.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<CommandResponse<Domain.Tournament>> CreateTournament(CreateTournamentCommand command);
    }
}
