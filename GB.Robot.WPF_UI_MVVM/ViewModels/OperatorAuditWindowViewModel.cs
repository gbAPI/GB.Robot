using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    class OperatorAuditWindowViewModel: ViewModel
    {
        #region Title: string - Название окна
        private string _Title = "Аудит запросов";
        /// <summary>Название окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

    }
}
