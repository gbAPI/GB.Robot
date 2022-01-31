using GB.Robot.WPF_UI_MVVM.Infrastructure;
using GB.Robot.WPF_UI_MVVM.Infrastructure.Commands;
using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using Robot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    internal class OperatorWindowViewModel : ViewModel
    {
        private readonly IRulesService _rulesService;
        private readonly IExternalObjectsService _externalObjectsService;

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

        #region RulesList: List<BO_Rule> - Список решений
        private List<BO_Rule> _RulesList = default;

        /// <summary>Список решений</summary>
        public List<BO_Rule> RulesList
        {
            get => _RulesList;
            set => Set(ref _RulesList, value);
        }
        #endregion

        #region SelectedRule: BO_Rule - Выбранное решение
        private BO_Rule _SelectedRule = new() { ID=-1};
        /// <summary>Выбранное правило</summary>
        public BO_Rule SelectedRule
        {
            get => _SelectedRule;
            set
            {
                if (Set(ref _SelectedRule, value))
                    if (value != null) 
                    {
                        if (value.Template != null)
                        {
                            for (int i = 0; i <= TemplatesList.Count - 1; i++)
                            {
                                if (value.Template.ID == TemplatesList[i].ID)
                                    SelectedTemplateIndex = i;
                            }
                        }
                        else
                        {
                            SelectedTemplateIndex = -1;
                        }
                    }
                    
            }

        }
        #endregion

        #region SelectedField: BO_Field - Выделенное поле из списка решений
        private BO_Field _SelectedField = default;
        /// <summary>Выделенное поле из списка решений</summary>
        public BO_Field SelectedField
        {
            get => _SelectedField;
            set => Set(ref _SelectedField, value);
        }
        #endregion

        #region SelectedScanerField: BO_Field - Выделенное поле из списка полей
        private BO_Field _SelectedScanerField = default;
        /// <summary>Выделенное поле из списка полей</summary>
        public BO_Field SelectedScanerField
        {
            get => _SelectedScanerField;
            set => Set(ref _SelectedScanerField, value);
        }
        #endregion

        #region Description: string - Описание выделенного элемента
        private string _Description = default;
        /// <summary>Описание выделенного элемента</summary>
        public string Description
        {
            get => _Description;
            set => Set(ref _Description, value);
        }
        #endregion

        #region RuleDescription: string - Описание решения
        private string _RuleDescription = default;
        /// <summary>Описание решения</summary>
        public string RuleDescription
        {
            get => _RuleDescription;
            set => Set(ref _RuleDescription, value);
        }
        #endregion

        #region FieldsList: List<BO_Field> - Список полей
        private List<BO_Field> _FieldsList = default;
        /// <summary>Список полей</summary>
        public List<BO_Field> FieldsList
        {
            get => _FieldsList;
            set => Set(ref _FieldsList, value);
        }
        #endregion

        #region TemplatesList: List<BO_Template> - Список шаблонов
        private List<BO_Template> _TemplatesList = default;
        /// <summary>Список шаблонов</summary>
        public List<BO_Template> TemplatesList
        {
            get => _TemplatesList;
            set => Set(ref _TemplatesList, value);
        }
        #endregion

        #region SelectedTemplate: BO_Template - Выбранный шаблон
        private BO_Template _SelectedTemplate = default;
        /// <summary>Выбранный шаблон</summary>
        public BO_Template SelectedTemplate
        {
            get => _SelectedTemplate;
            set => Set(ref _SelectedTemplate, value);
        }
        #endregion

        #region SelectedTemplateIndex: int - Индекс выбранного шаблона
        private int _SelectedTemplateIndex = 0;
        /// <summary>Индекс выбранного шаблона</summary>
        public int SelectedTemplateIndex
        {
            get => _SelectedTemplateIndex;
            set => Set(ref _SelectedTemplateIndex, value);
        }
        #endregion


        #region SelectedCommand - Выполняется при выделении элемента
        /// <summary>Выполняется при выделении элемента</summary>
        public LambdaCommand SelectedCommand { get; }
        private void OnSelectedCommandExecuted(object sender)
        {
            if (sender is BO_Rule)
                RuleDescription = (sender as BO_Rule).Description;
            if (sender != null)
            {
                object description = sender.GetType().GetProperty("Description", typeof(string))?.GetValue(sender);

                Description = (string)description ?? "Описание отсутствует";
            }
        }
        #endregion

        #region DropDownOpenCommand - Команда выполняемая при открытии списка
        /// <summary>Команда выполняемая при открытии списка</summary>
        public LambdaCommand DropDownOpenCommand { get; }
        private void OnDropDownOpenCommandExecuted()
        {
            RulesList = _rulesService.GetAll().ToList();
        }
        #endregion

        #region NewRule - Создаем новое решение
        /// <summary>Создаем новое решение</summary>
        public LambdaCommand NewRule { get; }
        private void OnNewRuleExecuted()
        {
            SelectedRule = new BO_Rule() { ID=-1};
            Description = "";
            RuleDescription = "";
        }
        #endregion

        #region AddFieldToRule - Добавляем поле в решение
        /// <summary>Добавляем поле в решение</summary>
        public LambdaCommand AddFieldToRule { get; }
        private void OnAddFieldToRuleExecuted()
        {
            if (SelectedRule.RequiredFields.FirstOrDefault(f => f.Name == SelectedScanerField.Name) == null)
            {
                SelectedRule.RequiredFields.Add(SelectedScanerField);
                //var tmp = SelectedRule;
                SelectedRule = SelectedRule.Clone();
            }
        }
        #endregion

        #region SetTemplate - Указать шаблон для решения
        /// <summary>Указать шаблон для решения</summary>
        public LambdaCommand SetTemplate { get; }
        private void OnSetTemplateExecuted()
        {
            if (SelectedRule.Template != SelectedTemplate)
            {
                SelectedRule.Template = SelectedTemplate;
                SelectedRule = SelectedRule.Clone();
            }
        }
        #endregion

        #region SetDescription - Устанавливает описание решению
        /// <summary>Устанавливает описание решению</summary>
        public LambdaCommand SetDescription { get; }
        private void OnSetDescriptionExecuted()
        {
            if (!SelectedRule.Description.Equals(Description))
            {
                SelectedRule.Description = Description;
                SelectedRule = SelectedRule.Clone();
            }
        }
        #endregion

        #region SaveRule - Сохраняем изменения решения
        /// <summary>Сохраняем изменения решения</summary>
        public LambdaCommand SaveRule { get; }
        private void OnSaveRuleExecuted()
        {
            if (string.IsNullOrWhiteSpace(SelectedRule.Name))
                return;
            if (SelectedRule.RequiredFields.Count <= 0)
                return;
            if (SelectedRule.Template == null)
                return;
            if (string.IsNullOrWhiteSpace(SelectedRule.DocumentType))
                return;

            if (SelectedRule.ID == -1)
                _rulesService.Add(SelectedRule);
            else
                _rulesService.Update(SelectedRule);

        }
        #endregion

        public OperatorWindowViewModel(IRulesService rulesService, IExternalObjectsService externalObjectsService)
        {
            #region Инициализация команд
            DropDownOpenCommand = new(OnDropDownOpenCommandExecuted);
            SelectedCommand = new(OnSelectedCommandExecuted);
            NewRule = new(OnNewRuleExecuted);
            AddFieldToRule = new(OnAddFieldToRuleExecuted);
            SetTemplate = new(OnSetTemplateExecuted);
            SetDescription = new(OnSetDescriptionExecuted);
            SaveRule = new(OnSaveRuleExecuted);
            #endregion

            _rulesService = rulesService;
            _externalObjectsService = externalObjectsService;

            FieldsList = _externalObjectsService.GetScannerFields().ToList();
            TemplatesList = _externalObjectsService.GetTAllTemplates().ToList();
        }
    }
}
