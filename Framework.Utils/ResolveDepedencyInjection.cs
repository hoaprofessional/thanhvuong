using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Utils
{
    public class ResolveDepedencyInjection
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static T Resolve<T>()
        {
            return (T)ServiceProvider.GetService(typeof(T));
        }
    }
}
