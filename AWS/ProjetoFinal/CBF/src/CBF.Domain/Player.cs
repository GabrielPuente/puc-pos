using CBF.Domain.DefaultEntity;
using Flunt.Validations;
using System;
using System.Diagnostics;

namespace CBF.Domain
{
    [DebuggerDisplay("Name: {Name}")]
    public class Player : Entity
    {
        public Guid TeamId { get; private set; }
        
        public string Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string Country  { get; private set; }

        public decimal MarketValue { get; private set; }

        public Player(Guid teamId, string name, DateTime birthDate, string country, decimal marketValue)
        {
            TeamId = teamId;
            Name = name;
            BirthDate = birthDate;
            Country = country;
            MarketValue = marketValue;

            CheckDomainIsValid();
        }

        public void ChangeTeam(Guid teamId)
        {
            TeamId = teamId;
        }

        public void ChangeMarketValue(decimal marketValue)
        {
            MarketValue = marketValue;
        }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Player>()
                  .IsNotNull(TeamId, "TeamId", "Obrigatorio associar a um time")
                  .IsNotNullOrEmpty(Name, "Name", "Campo nome é obrigatorio")
                  .IsGreaterThan(BirthDate,DateTime.MinValue, "BirthDate", "Campo data de nascimento invalida")
                  .IsGreaterThan(MarketValue,0, "MarketValue", "Campo valor de mercado é obrigatorio e deve ser maior que zero")
                  .IsNotNullOrEmpty(Country, "Country", "Campo localidade é obrigatorio"));
        }
    }
}
