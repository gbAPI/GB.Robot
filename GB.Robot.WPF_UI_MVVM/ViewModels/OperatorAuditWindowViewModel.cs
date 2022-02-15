using GB.Robot.WPF_UI_MVVM.Infrastructure.Commands;
using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using Robot.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    class OperatorAuditWindowViewModel : ViewModel
    {
        private readonly IRulesService _rulesService;
        private readonly IQueriesService _queriesService;


        #region Title: string - Название окна
        private string _Title = "Аудит запросов";


        /// <summary>Название окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Height: int - Высота окна
        private int _height = 450;
        /// <summary>Высота окна</summary>
        public int Height
        {
            get => _height;
            set => Set(ref _height, value);
        }
        #endregion

        #region Width: int - Ширина окна
        private int _width = 800;
        /// <summary>Ширина окна</summary>
        public int Width
        {
            get => _width;
            set => Set(ref _width, value);
        }
        #endregion

        #region RulesList: List<BO_Rule> - Список решений
        private List<BO_Rule> _rulesList = new();
        /// <summary>Deafault Comment</summary>
        public List<BO_Rule> RulesList
        {
            get => _rulesList;
            set => Set(ref _rulesList, value);
        }
        #endregion

        #region FieldsRule: List<BO_Field> - Поля решения
        private List<BO_Field> _fieldsRule = default;
        /// <summary>Поля решения</summary>
        public List<BO_Field> FieldsRule
        {
            get => _fieldsRule;
            set => Set(ref _fieldsRule, value);
        }
        #endregion

        #region QueriesList: List<BO_Query> - Список запросов
        private List<BO_Query> _queriesList = default;
        /// <summary>Список запросов</summary>
        public List<BO_Query> QueriesList
        {
            get => _queriesList;
            set => Set(ref _queriesList, value);
        }
        #endregion

        #region SelectedQuery: BO_Query - Выделенный запрос
        private BO_Query _selectedQuery = default;
        /// <summary>Выделенный запрос</summary>
        public BO_Query SelectedQuery
        {
            get => _selectedQuery;
            set => Set(ref _selectedQuery, value);
        }
        #endregion


        #region UpdateQueriesList - Обновляем список поступивших запросов
        /// <summary>Обновляем список поступивших запросов</summary>
        public LambdaCommandAsync UpdateQueriesListCommand { get; }
        private async Task OnUpdateQueriesListCommandExecuted()
        {
            await Task.Run(() => QueriesList = _queriesService.GetAll().ToList());
        }
        #endregion

        public OperatorAuditWindowViewModel(IRulesService rulesService, IQueriesService queriesService)
        {
            _rulesService = rulesService;
            _queriesService = queriesService;

            UpdateQueriesListCommand = new LambdaCommandAsync(OnUpdateQueriesListCommandExecuted);

            QueriesList = _queriesService.GetAll().ToList();
            //RulesList = _rulesService.GetAll().ToList();
            //FieldsRule = RulesList[0].RequiredFields;
            //FieldsRule.AddRange(FieldsRule);
            //FieldsRule.AddRange(FieldsRule);
        }
    }
}
