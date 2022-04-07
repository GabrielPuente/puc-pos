using System;

namespace CBF.Application.ViewModels
{
    public class EventViewModel
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public DateTime Reference { get; set; }
    }
}
