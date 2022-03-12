using System;

namespace CBF.Application.ViewModels
{
    public class TransferViewModel
    {
        public Guid Id { get; set; }

        public Guid TeamOriginId { get; set; }

        public Guid TeamDestinyId { get; set; }

        public string TeamOriginName { get; set; }

        public string TeamDestinyName { get; set; }
        
        public Guid PlayerId { get; set; }

        public string PlayerName { get; set; }

        public decimal Value { get; set; }

        public DateTime TransferDate { get; set; }
    }
}
