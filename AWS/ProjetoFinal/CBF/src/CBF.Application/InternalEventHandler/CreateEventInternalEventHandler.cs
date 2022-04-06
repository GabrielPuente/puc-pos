using CBF.Application.InternalEvent;
using CBF.Infra.Data.Interfaces;
using Rebus.Handlers;
using System.Linq;
using System.Threading.Tasks;

namespace CBF.Application.InternalEventHandler
{
    public class CreateEventInternalEventHandler : IHandleMessages<CreateEventInternalEvent>
    {
        private readonly ITournamentRepository _repository;

        public CreateEventInternalEventHandler(ITournamentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateEventInternalEvent message)
        {
            var tournament = await _repository.GetWithIncludes(message.TournamentId);

            if (tournament is null) return;

            var evt = new Domain.Event(message.Message);
            tournament.Matches.FirstOrDefault(x => x.Id == message.MatchId).AddEvent(evt);

            await _repository.Update(tournament);
            await _repository.SaveChanges();
        }
    }
}
