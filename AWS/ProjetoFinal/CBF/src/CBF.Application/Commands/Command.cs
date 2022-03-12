using Flunt.Notifications;

namespace CBF.Application.Commands
{
    public abstract class Command : Notifiable<Notification>
    {
        public abstract void Validate();
    }
}
