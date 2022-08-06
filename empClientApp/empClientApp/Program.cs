using System;
using empClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
namespace empClientApp
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            ConfigureServices();
            _serviceProvider.GetService<Executer>().Run();
            DisposeServices();
        }

        static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ITokenManager, TokenManager>().
                AddSingleton<Executer, Executer>()
                .AddSingleton<IEmployee, EmployeeService>();




            _serviceProvider = services.BuildServiceProvider();

        }

        static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
