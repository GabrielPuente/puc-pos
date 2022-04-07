using System;
using System.Collections.Generic;

namespace CBF.Application.ViewModels
{
    public class TournamentViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Reference { get; set; }

        public List<MatchViewModel> Matchs { get; set; } = new();
    }
}
