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

        public string Role { get; private set; }

        protected User()
        {
        }

        public User(string name, string country, DateTime birthDate, string document, string email, string password, string role)
        {
            Name = name;
            Country = country;
            BirthDate = birthDate;
            Document = document;
            Email = email;
            Password = password;
            Role = role;

            CheckDomainIsValid();
        }

        private void CheckDomainIsValid()
        {
            AddNotifications(new Contract<Notification>()
                  .IsNotNullOrEmpty(Name, "Name", "Campo nome é obrigatorio")
                  .IsEmail(Email, "Email", "Email invalido")
                  .IsCpf(Document, "Document", "Documento invalido")
                  .IsNotNullOrEmpty(Password, "Password", "Senha é obrigatorio")
                  .IsNotNullOrEmpty(Role, "Role", "Campo cargo é obrigatorio")
                  .IsGreaterThan(BirthDate,DateTime.MinValue, "BirthDate", "Campo data de nascimento invalida")
                  .IsNotNullOrEmpty(Country, "Country", "Campo localidade é obrigatorio"));
        }
    }
}
