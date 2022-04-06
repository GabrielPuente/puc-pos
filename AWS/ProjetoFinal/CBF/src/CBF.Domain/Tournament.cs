using CBF.Domain.DefaultEntity;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace CBF.Domain
{
    public class Tournament : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public DateTime Reference { get; private set; }

        private readonly List<Match> _matches = new();

        public virtual IReadOnlyList<Match> Matches => _matches.AsReadOnly();

        protected Tournament()
        {
        }

        public Tournament(string name, DateTime reference)
        {
            Name = name;
            Reference = reference;

            CheckDomainIsValid();
        }

        public void AddMatch(Match match)
        {
            _matches.Add(match);
        }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Player>()
                  .IsNotNullOrEmpty(Name, "Name", "Campo nome é obrigatorio")
                  .IsGreaterThan(Reference, DateTime.MinValue, "Reference", "Data da partida é obrigatorio"));
        }
    }
}
