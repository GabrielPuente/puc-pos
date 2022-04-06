using System;

namespace CBF.Application.InternalEvent
{
    public class CreateEventInternalEvent
    {
        public Guid TournamentId { get; set; }

        public Guid MatchId { get; set; }

        public string Message { get; set; }
    }
}
