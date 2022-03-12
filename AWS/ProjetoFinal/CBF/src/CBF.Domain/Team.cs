using CBF.Domain.DefaultEntity;
using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;
using System.Diagnostics;

namespace CBF.Domain
{
    [DebuggerDisplay("Name: {Name}")]
    public class Team : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Locality { get; private set; }

        private readonly List<Player> _players = new();

        public IReadOnlyList<Player> Players => _players.AsReadOnly();

        protected Team()
        {

        }

        public Team(string name, string locality)
        {
            Name = name;
            Locality = locality;

            CheckDomainIsValid();
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public void DeletePlayer(Player player)
        {
            _players.Remove(player);
        }

        public void CheckDomainPlayersIsValid()
        {
            foreach (var player in Players)
            {
                AddNotifications(player.Notifications);
            }
        }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Team>()
                  .IsCpf(Name, "Name", "Campo nome é obrigatorio")
                  .IsNotNullOrEmpty(Locality, "Locality", "Campo localidade é obrigatorio"));
        }
    }
}