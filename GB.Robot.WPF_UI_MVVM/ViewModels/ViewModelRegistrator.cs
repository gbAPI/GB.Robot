using Microsoft.Extensions.DependencyInjection;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
           .AddSingleton<MainWindowViewModel>()
           .AddSingleton<LoginWindowViewModel>()
        ;
    }
}