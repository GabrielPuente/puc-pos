using CBF.Application.Commands;
using Flunt.Notifications;
using System.Collections.Generic;

namespace CBF.Application.CommandHandlers
{
    public abstract class CommandHandler
    {
        protected virtual CommandResponse Ok()
        {
            return new CommandResponse(new List<Notification>().AsReadOnly());
        }

        protected virtual CommandResponse Ok(IReadOnlyCollection<Notification> notifications)
        {
            return new CommandResponse(notifications);
        }

        protected virtual CommandResponse<TData> Ok<TData>(TData data, IReadOnlyCollection<Notification> notifications)
        {
            return new CommandResponse<TData>(data, notifications);
        }

        protected virtual CommandResponse<TData> Ok<TData>(TData data)
        {
            return new CommandResponse<TData>(data);
        }

        protected virtual CommandResponse Fail(IReadOnlyCollection<Notification> notifications)
        {
            return new CommandResponse(notifications);
        }

        protected virtual CommandResponse Fail(string message, IReadOnlyCollection<Notification> notifications)
        {
            return new CommandResponse(notifications, message);
        }

        protected virtual CommandResponse<TData> Fail<TData>(IReadOnlyCollection<Notification> notifications)
        {
            return new CommandResponse<TData>(default(TData), notifications);
        }

        protected virtual CommandResponse<TData> Fail<TData>(string message, IReadOnlyCollection<Notification> notifications)
        {
            return new CommandResponse<TData>(default(TData), notifications, message);
        }
    }
}