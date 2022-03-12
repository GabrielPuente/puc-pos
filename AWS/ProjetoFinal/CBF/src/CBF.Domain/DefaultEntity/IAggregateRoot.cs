using System;

namespace CBF.Domain.DefaultEntity
{
    public interface IAggregateRoot
    {
        Guid Id
        {
            get;
        }
    }
}