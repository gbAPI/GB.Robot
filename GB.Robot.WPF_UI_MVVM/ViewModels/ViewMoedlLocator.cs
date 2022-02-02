using Microsoft.Extensions.DependencyInjection;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    internal class ViewModelLocator
    {
        public LoginWindowViewModel LoginWindowModel => App.Services.GetRequiredService<LoginWindowViewModel>();
        public OperatorWindowViewModel OperatorWindowModel => App.Services.GetRequiredService<OperatorWindowViewModel>();
        public AdministratorWindowViewModel AdministratorWindowModel =>
            App.Services.GetRequiredService<AdministratorWindowViewModel>();
        public OperatorAuditWindowViewModel OperatorAuditWindowModel =>
            App.Services.GetRequiredService<OperatorAuditWindowViewModel>();
    }
}
