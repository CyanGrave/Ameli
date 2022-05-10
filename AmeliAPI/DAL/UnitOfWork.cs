using ModelBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _Disposed = false;
        private Context _Context;
        private readonly Dictionary<Type, Repository> _Repositories = new Dictionary<Type, Repository>();


        public UnitOfWork(Context context)
        {
            _Context = context;
        }

        public async Task SaveAsync()
        {
            await _Context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
                if (disposing)
                {
                    _Context?.Dispose();
                    foreach (var rep in _Repositories.Values)
                        rep?.Dispose();
                }

            this._Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase
        {
            if (!_Repositories.ContainsKey(typeof(TEntity)))
                _Repositories.Add(typeof(TEntity), new Repository<TEntity>(this._Context));
            return _Repositories[typeof(TEntity)] as Repository<TEntity>;
        }
    }
}
