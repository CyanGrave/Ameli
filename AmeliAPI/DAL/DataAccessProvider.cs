using System;
using System.Collections.Generic;
using System.Text;
using ModelBase;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataAccessProvider<TContext> : IDataAccessProvider where TContext : Context
    {
        private DbContextOptions _options;

        public DataAccessProvider(DbContextOptions options)
        {
            _options = options;
        }

        public IRepository GetRepository()
        {
            return new Repository(CreateContext(_options));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase
        {
            return new Repository<TEntity>(CreateContext(_options));
        }

        public IUnitOfWork NewUnitOfWork()
        {
            return new UnitOfWork(CreateContext(_options));
        }

        private Context CreateContext(DbContextOptions options)
        {
            var context = Activator.CreateInstance(typeof(TContext), new object[] { options }) as Context;
            if (context is null)
                throw new Exception($"Context Creation Failed for type {typeof(TContext).Name}");

            return context;
        }
    }
}
