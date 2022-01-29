using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    internal class OperatorWindowViewModel:ViewModel
    {
        #region Title: string - Название окна
        private string _Title = "Оператор робота";
        /// <summary>Название окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region TreeList: List<string> - Список решений
        private List<string> _TreeList = new() {"1","2","3" };
        /// <summary>Список решений</summary>
        public List<string> TreeList
        {
            get => _TreeList;
            set => Set(ref _TreeList, value);
        }
        #endregion

    }
}
