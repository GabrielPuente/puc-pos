using CBF.Domain.DefaultEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBF.Infra.Data.Interfaces
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll(bool trackEntities = true);

        Task<T> Get(Guid id, bool trackEntities = true);

        Task<bool> Add(T entity);

        Task<bool> Update(T entity);

        Task Delete(T entity);

        Task SaveChanges();
    }
}
