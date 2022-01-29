using GB.Robot.WPF_UI_MVVM.Infrastructure.Commands;
using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    internal class OperatorWindowViewModel : ViewModel
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
        private List<string> _TreeList = new() { "1", "2", "3" };
        /// <summary>Список решений</summary>
        public List<string> TreeList
        {
            get => _TreeList;
            set => Set(ref _TreeList, value);
        }
        #endregion

        #region DropDownOpenCommand - Команда выполняемая при открытии списка
        /// <summary>Команда выполняемая при открытии списка</summary>
        public LambdaCommand DropDownOpenCommand { get; }
        private void OnDropDownOpenCommandExecuted()
        {
            Random random = new();
            TreeList = new() { $"{random.Next(10)}", $"{random.Next(10)}", $"{random.Next(10)}", $"{random.Next(10)}" };
        }
        #endregion


        public OperatorWindowViewModel()
        {
            DropDownOpenCommand = new LambdaCommand(OnDropDownOpenCommandExecuted);
        }
    }
}
