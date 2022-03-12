using CBF.Domain.DefaultEntity;
using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CBF.Domain
{
    public class User : Entity
    {
        public string Name { get; private set; }

        public string Country  { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string Document { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Notification>()
                  .IsEmail(Email, "Email", "Email invalido")
                  .IsCpf(Document, "Document", "Documento invalido")
                  .IsNullOrEmpty(Name, "Name", "Campo nome é obrigatorio")
                  .IsGreaterThan(BirthDate,DateTime.MinValue, "BirthDate", "Campo data de nascimento invalida")
                  .IsNotNullOrEmpty(Country, "Country", "Campo localidade é obrigatorio"));
        }
    }
}
