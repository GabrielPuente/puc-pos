using CBF.Domain.DefaultEntity;
using Flunt.Validations;
using System;

namespace CBF.Domain
{
    public class Match : Entity , IAggregateRoot
    {
        public Tournament Tournament { get; private set; }

        public Team HomeTeam { get; private set; }

        public Team AwayTeam { get; private set; }

        public DateTime Reference { get; private set; }

        protected Match()
        {
        }

        public Match(Tournament tournament, Team homeTeam, Team awayTeam, DateTime reference)
        {
            Tournament = tournament;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Reference = reference;

            CheckDomainIsValid();
        }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Player>()
                  .IsNotEmpty(Tournament.Id, "Tournament", "Torneio é obrigatorio")
                  .IsNotEmpty(HomeTeam.Id, "HomeTeam", "Time de casa é obrigatorio")
                  .IsNotEmpty(AwayTeam.Id, "AwayTeam", "Time fora de casa é obrigatorio")
                  .IsGreaterThan(Reference, DateTime.MinValue, "Reference", "Data da partida é obrigatorio"));
        }
    }
}
