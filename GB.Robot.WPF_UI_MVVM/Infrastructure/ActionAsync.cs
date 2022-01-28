using System.Threading.Tasks;

namespace GB.Robot.WPF_UI_MVVM.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
