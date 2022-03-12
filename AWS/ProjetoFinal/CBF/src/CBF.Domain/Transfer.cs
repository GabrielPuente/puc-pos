using CBF.Domain.DefaultEntity;
using Flunt.Validations;
using System;

namespace CBF.Domain
{
    
    public class Transfer : Entity
    {
        public Team TeamOrigin { get; private set; }

        public Team TeamDestiny { get; private set; }

        public Player Player { get; private set; }

        public DateTime TransferDate { get; private set; }

        public decimal Value { get; private set; }

        protected Transfer()
        {
        }

        public Transfer(Team teamOrigin, Team teamDestiny, Player player, decimal value)
        {
            TeamOrigin = teamOrigin;
            TeamDestiny = teamDestiny;
            Player = player;
            Value = value;
            TransferDate = DateTime.UtcNow;

            CheckDomainIsValid();
        }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Transfer>()
                  .IsNotEmpty(TeamOrigin.Id, "TeamOrigin.Id", "Obrigatorio associar a um time")
                  .IsNotEmpty(TeamDestiny.Id, "TeamDestiny.Id", "Campo nome é obrigatorio")
                  .IsNotEmpty(Player.Id, "Player.Id", "Campo data de nascimento invalida")
                  .IsGreaterThan(Value, 0, "Value", "Campo localidade é obrigatorio"));
        }
    }
}