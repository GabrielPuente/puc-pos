using System;
using System.Collections.Generic;

namespace CBF.Application.ViewModels
{
    public class TeamViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Locality { get; set; }

        public List<PlayerViewModel> Players { get; set; }

        public TeamViewModel()
        {
            Players = new List<PlayerViewModel>();
        }
    }
}
