using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using Robot.Core;
using System.Collections.Generic;
using System.Linq;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    class OperatorAuditWindowViewModel : ViewModel
    {
        private readonly IRulesService _rulesService;


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


        public OperatorAuditWindowViewModel(IRulesService rulesService)
        {
            _rulesService = rulesService;

            RulesList = _rulesService.GetAll().ToList();
            FieldsRule = RulesList[0].RequiredFields;
            FieldsRule.AddRange(FieldsRule);
            FieldsRule.AddRange(FieldsRule);
        }
    }
}
