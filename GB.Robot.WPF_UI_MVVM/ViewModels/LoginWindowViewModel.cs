using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using System;
using System.Timers;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    class LoginWindowViewModel : ViewModel
    {
        private readonly Timer _timer;

        #region Title: string - Название окна
        private string _Title = "Робот-процессор";
        /// <summary>Название окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Tim: string - Время
        private string _tim = default;
        /// <summary>Время</summary>
        public string Tim
        {
            get => _tim;
            set => Set(ref _tim, value);
        }
        #endregion

        public LoginWindowViewModel()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) => Tim = DateTime.Now.ToString("G");
    }
}
