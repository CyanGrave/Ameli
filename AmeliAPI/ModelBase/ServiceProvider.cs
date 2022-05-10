using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelBase
{

    public class ServiceProvider
    {
        private Dictionary<Type, Tuple<Type,IDataAccessProvider>> _serviceRegister = new Dictionary<Type, Tuple<Type, IDataAccessProvider>>();

        private static ServiceProvider _current;
        public static ServiceProvider Current
        {
            get
            {
                if (_current is null)
                    Initialize();
                return _current;
            }
        }


        private static void Initialize()
        {
            _current = new ServiceProvider();

        }


        private ServiceProvider()  { }

        public void Register<TImplementation>(IDataAccessProvider dataAccessProvider) where TImplementation : ServiceBase
        {
            if (!_serviceRegister.ContainsKey(typeof(TImplementation)))
                _serviceRegister.Add(typeof(TImplementation), new Tuple<Type, IDataAccessProvider>( typeof(TImplementation), dataAccessProvider ));
        }

        public void Register<TInterface1, TImplementation>(IDataAccessProvider dataAccessProvider) where TImplementation : ServiceBase, TInterface1
        {
            if (!_serviceRegister.ContainsKey(typeof(TInterface1)))
                _serviceRegister.Add(typeof(TInterface1), new Tuple<Type, IDataAccessProvider>(typeof(TImplementation), dataAccessProvider));

            Register<TImplementation>(dataAccessProvider);
        }

        public void Register<TInterface1, TInterface2, TImplementation>(IDataAccessProvider dataAccessProvider) where TImplementation : ServiceBase, TInterface1, TInterface2
        {
            if (!_serviceRegister.ContainsKey(typeof(TInterface2)))
                _serviceRegister.Add(typeof(TInterface2), new Tuple<Type, IDataAccessProvider>(typeof(TImplementation), dataAccessProvider));

            Register<TInterface1, TImplementation>(dataAccessProvider);
        }

        public void Register<TInterface1, TInterface2, TInterface3, TImplementation>(IDataAccessProvider dataAccessProvider) where TImplementation : ServiceBase, TInterface1, TInterface2, TInterface3
        {
            if (!_serviceRegister.ContainsKey(typeof(TInterface3)))
                _serviceRegister.Add(typeof(TInterface3), new Tuple<Type, IDataAccessProvider>(typeof(TImplementation), dataAccessProvider));

            Register<TInterface1, TInterface2, TImplementation>(dataAccessProvider);
        }



        public TService Get<TService>()
        {
            if (!_serviceRegister.ContainsKey(typeof(TService)))
                throw new Exception($"no Service registered for {typeof(TService).Name}");

            var t = _serviceRegister[typeof(TService)];
            var ctor = t.Item1.GetConstructor(new[] { typeof(IDataAccessProvider) });

            if(ctor is null)
                throw new Exception($"No Ctor with {nameof(IDataAccessProvider)} found in service implementation for {typeof(TService).Name}");

            return (TService) ctor.Invoke(new[] { t.Item2 });

        }



    }
}
