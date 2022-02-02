using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using LiveCharts;
using System;

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

        #region CValue: ChartValues<int> - Значения для графика
        private ChartValues<int> _cValue = new() { 1,5,7,6,3,4,5,9};
        /// <summary>Значения для графика</summary>
        public ChartValues<int> CValue
        {
            get => _cValue;
            set => Set(ref _cValue, value);
        }
        #endregion

        #region StartDate: DateTime - Начальная дата
        private DateTime _startDate = default;
        /// <summary>Начальная дата</summary>
        public DateTime StartDate
        {
            get => _startDate;
            set => Set(ref _startDate, value);
        }
        #endregion

        #region EndDate: DateTime - Конечная дата
        private DateTime _endDate = default;
        /// <summary>Конечная дата</summary>
        public DateTime EndDate
        {
            get => _endDate;
            set => Set(ref _endDate, value);
        }
        #endregion

    }
}
