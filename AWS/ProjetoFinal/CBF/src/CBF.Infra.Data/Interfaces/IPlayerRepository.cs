using CBF.Domain;
using System;
using System.Threading.Tasks;

namespace CBF.Infra.Data.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> Get(Guid id);

        public void Update(Player player);

        Task SaveChanges();
    }
}
