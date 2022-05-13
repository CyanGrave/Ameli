using System;
using System.Collections.Generic;
using System.Text;
using ModelBase;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataAccessProvider<TContext> : IDataAccessProvider where TContext : Context, new()
    {
        private ContextFactory<TContext> _contextFactory;

        public DataAccessProvider(ContextFactory<TContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IRepository GetRepository()
        {
            return new Repository(_contextFactory.CreateDbContext());
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase
        {
            return new Repository<TEntity>(_contextFactory.CreateDbContext());
        }

        public IUnitOfWork NewUnitOfWork()
        {
            return new UnitOfWork(_contextFactory.CreateDbContext());
        }


    }
}
