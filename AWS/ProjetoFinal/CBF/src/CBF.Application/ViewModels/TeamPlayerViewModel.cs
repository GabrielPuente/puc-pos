using System;

namespace CBF.Application.ViewModels
{
    public class TeamPlayerViewModel
    {
        public Guid PlayerId { get; set; }

        public Guid TeamId { get; set; }

        public string PlayerName { get; set; }

        public string TeamName { get; set; }

        public decimal MarketValue { get; set; }
    }
}
