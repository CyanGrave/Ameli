using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelBase
{
    public class ServiceBase : IServiceBase
    {
        public readonly IDataAccessProvider _DataAccessProvider;

        public ServiceBase(IDataAccessProvider dataAccessProvider)
        {
            _DataAccessProvider = dataAccessProvider;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}
