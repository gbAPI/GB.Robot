using Common.RMQServices;
using Common.RMQServices.Interfaces;
using GB.Robot.WPF_UI_MVVM.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Robot.Core;
using Robot.Core.Services;

namespace GB.Robot.WPF_UI_MVVM.Services
{
    static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>()
           .AddTransient<IRobotRabbitService, RobotRabbitService>()
           .AddScoped<IRulesService, RulesService>()
           .AddScoped<IExternalObjectsService, ExternalObjectsService>()
           .AddScoped<IQueriesService, QueriesService>()
           .AddScoped<IProcessingService, ProcessingService>()
           .AddScoped<IScanerRabbitService, ScanerRabbitService>()
        ;
    }
}
