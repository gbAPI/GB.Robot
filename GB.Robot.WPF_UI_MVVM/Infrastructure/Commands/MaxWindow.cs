using GB.Robot.WPF_UI_MVVM.Infrastructure.Commands.Base;
using System.Windows;

namespace GB.Robot.WPF_UI_MVVM.Infrastructure.Commands
{
    class MaxWindow : Command
    {
        private static Window GetWindow(object p) => p as Window ?? App.FocusedWindow ?? App.ActivedWindow;

        protected override bool CanExecute(object p) => GetWindow(p) != null;

        protected override void Execute(object p)
        {
            Window w = GetWindow(p);
            if (w != null)
            {
                if (w.WindowState != WindowState.Maximized)
                    w.WindowState = WindowState.Maximized;
                else w.WindowState = WindowState.Normal;
            }
        }
    }
}
