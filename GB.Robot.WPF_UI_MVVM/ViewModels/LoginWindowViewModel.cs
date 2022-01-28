using GB.Robot.WPF_UI_MVVM.ViewModels.Base;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    class LoginWindowViewModel : ViewModel
    {
        #region Title: string - Название окна
        private string _Title = "Робот-процессор";
        /// <summary>Название окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

    }
}
