using System;

namespace CBF.Application.ViewModels
{
    public class PlayerViewModel
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Country { get; set; }

        public decimal MarketValue { get; set; }
    }
}
