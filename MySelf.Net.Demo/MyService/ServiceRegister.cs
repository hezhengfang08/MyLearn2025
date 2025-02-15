using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySelf.Net.Demo.MyInterface;

namespace MySelf.Net.Demo.MyService
{
    public static class ServiceRegister
    {
        public static void RegisterNET7Service(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITestServiceA, TestServiceA>();//瞬时
            serviceCollection.AddSingleton<ITestServiceB, TestServiceB>();//单例
            serviceCollection.AddScoped<ITestServiceC, TestServiceC>();//作用域单例--一次请求一个实例,
            //作用域其实依赖于ServiceProvider（这个自身是根据请求的），跟多线程没关系

            serviceCollection.AddTransient<ITestServiceD, TestServiceD>();
            //builder.Services.AddTransient<ITestServiceD>(new TestServiceD());
            //builder.Services.AddTransient(typeof(ITestServiceD), () => new TestServiceD())
            //可以直接注册泛型，也可以注册类型(反射加载)，可以注册实例，可以注册委托--没能给出实例

            serviceCollection.AddTransient<ITestServiceE, TestServiceE>();

            serviceCollection.AddTransient<ITestServiceE, TestServiceEV2>();
        }
    }
}
