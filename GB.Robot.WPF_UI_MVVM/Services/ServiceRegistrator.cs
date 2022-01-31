using GB.Robot.WPF_UI_MVVM.Services.Interfaces;
using Robot.Core;
using Microsoft.Extensions.DependencyInjection;

namespace GB.Robot.WPF_UI_MVVM.Services
{
    static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>()
           .AddScoped<IRulesService, RulesService>()
           .AddScoped<IExternalObjectsService, ExternalObjectsService>()
        ;
    }
}
