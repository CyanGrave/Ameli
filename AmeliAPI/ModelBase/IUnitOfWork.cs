using System;
using System.Threading.Tasks;

namespace ModelBase
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase;
    }
}
