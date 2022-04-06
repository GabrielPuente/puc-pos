using CBF.Domain.DefaultEntity;
using Flunt.Validations;
using System;

namespace CBF.Domain
{
    public class Tournament : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public DateTime Reference { get; private set; }

        protected Tournament()
        {
        }

        public Tournament(string name, DateTime reference)
        {
            Name = name;
            Reference = reference;

            CheckDomainIsValid();
        }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Player>()
                  .IsNotNullOrEmpty(Name, "Name", "Campo nome é obrigatorio")
                  .IsGreaterThan(Reference, DateTime.MinValue, "Reference", "Data da partida é obrigatorio"));
        }
    }
}
