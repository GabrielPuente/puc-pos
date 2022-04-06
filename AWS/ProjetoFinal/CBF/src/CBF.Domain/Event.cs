using CBF.Domain.DefaultEntity;
using Flunt.Validations;
using System;

namespace CBF.Domain
{
    public class Event : Entity
    {
        public string Message { get; private set; }

        public DateTime Reference { get; private set; }

        protected Event()
        {

        }

        public Event(string message)
        {
            Message = message;
            Reference = DateTime.UtcNow;

            CheckDomainIsValid();
        }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Event>()
                  .IsNotNullOrEmpty(Message, "Message", "Campo menssagem é obrigatorio")
                  .IsGreaterThan(Reference, DateTime.MinValue, "Reference", "Data da partida é obrigatorio"));
        }
    }
}
