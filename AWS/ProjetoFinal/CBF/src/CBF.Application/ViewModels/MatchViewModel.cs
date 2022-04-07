using System;
using System.Collections.Generic;

namespace CBF.Application.ViewModels
{
    public class MatchViewModel
    {
        public Guid Id { get; set; }

        public Guid TournamentId { get; set; }
        
        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public DateTime Reference { get; set; }

        public List<EventViewModel> Events { get; set; } = new();
    }
}