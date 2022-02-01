using GB.Robot.WPF_UI_MVVM.ViewModels.Base;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    class AdministratorWindowViewModel : ViewModel
    {
        #region Title: string - Заголовок окна
        private string _title = "Администратор";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

    }
}
