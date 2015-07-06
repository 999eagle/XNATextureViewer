using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNATextureViewer
{
    public class ServiceContainer : IServiceProvider
    {
        Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void AddService<T>(T service)
        {
            services.Add(typeof(T), service);
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            object service;
            services.TryGetValue(serviceType, out service);
            return service;
        }
    }
}
