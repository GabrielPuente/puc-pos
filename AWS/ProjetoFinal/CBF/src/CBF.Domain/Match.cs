using CBF.Domain.DefaultEntity;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace CBF.Domain
{
    public class Match : Entity, IAggregateRoot
    {
        public Team HomeTeam { get; private set; }

        public Team AwayTeam { get; private set; }

        public DateTime Reference { get; private set; }

        private readonly List<Event> _events = new();

        public virtual IReadOnlyList<Event> Events => _events.AsReadOnly();

        protected Match()
        {
        }

        public Match(Team homeTeam, Team awayTeam, DateTime reference)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Reference = reference;

            CheckDomainIsValid();
        }

        public void AddEvent(Event evt)
        {
            _events.Add(evt);
        }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Player>()
                  .IsNotEmpty(HomeTeam.Id, "HomeTeam", "Time de casa é obrigatorio")
                  .IsNotEmpty(AwayTeam.Id, "AwayTeam", "Time fora de casa é obrigatorio")
                  .IsGreaterThan(Reference, DateTime.MinValue, "Reference", "Data da partida é obrigatorio"));
        }
    }
}
