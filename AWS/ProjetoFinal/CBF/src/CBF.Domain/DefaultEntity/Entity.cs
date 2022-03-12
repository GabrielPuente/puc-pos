using Flunt.Notifications;
using System;

namespace CBF.Domain.DefaultEntity
{
    public abstract class Entity : Notifiable<Notification>
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
