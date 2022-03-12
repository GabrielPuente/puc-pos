using System;

namespace CBF.Application.Commands.Team
{
    public class PlayerCommand
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Country { get; set; }

        public decimal MarketValue { get; set; }
    }
}
