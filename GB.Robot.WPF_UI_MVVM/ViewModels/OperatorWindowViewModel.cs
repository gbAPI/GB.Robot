using GB.Robot.WPF_UI_MVVM.Infrastructure;
using GB.Robot.WPF_UI_MVVM.Infrastructure.Commands;
using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using Robot.Core;
using System.Collections.Generic;
using System.Linq;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    internal class OperatorWindowViewModel : ViewModel
    {
        private readonly IRulesService _rulesService;
        private readonly IExternalObjectsService _externalObjectsService;

        #region Title: string - Название окна
        private string _title = "Оператор робота";
        /// <summary>Название окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
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
        private List<BO_Rule> _rulesList = default;

        /// <summary>Список решений</summary>
        public List<BO_Rule> RulesList
        {
            get => _rulesList;
            set => Set(ref _rulesList, value);
        }
        #endregion

        #region SelectedRule: BO_Rule - Выбранное решение
        private BO_Rule _selectedRule = new() { ID = -1 };
        /// <summary>Выбранное правило</summary>
        public BO_Rule SelectedRule
        {
            get => _selectedRule;
            set
            {
                if (Set(ref _selectedRule, value))
                {
                    if (value != null)
                    {
                        SelectedTemplate = TemplatesList.FirstOrDefault(x => x.Name == value.Template.Name);
                        SelectedDocType = value.DocumentType;
                    }
                }
            }

        }
        #endregion

        #region SelectedField: BO_Field - Выделенное поле решения
        private BO_Field _selectedField = default;
        /// <summary>Выделенное поле решения</summary>
        public BO_Field SelectedField
        {
            get => _selectedField;
            set => Set(ref _selectedField, value);
        }
        #endregion

        #region SelectedScanerField: BO_Field - Выделенное поле из списка полей
        private BO_Field _selectedScanerField = default;
        /// <summary>Выделенное поле из списка полей</summary>
        public BO_Field SelectedScanerField
        {
            get => _selectedScanerField;
            set => Set(ref _selectedScanerField, value);
        }
        #endregion

        #region Description: string - Описание выделенного элемента
        private string _description = default;
        /// <summary>Описание выделенного элемента</summary>
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }
        #endregion

        #region RuleDescription: string - Описание решения
        private string _ruleDescription = default;
        /// <summary>Описание решения</summary>
        public string RuleDescription
        {
            get => _ruleDescription;
            set
            {
                Set(ref _ruleDescription, value);
                SelectedRule.Description = value;
            }

        }
        #endregion

        #region FieldsList: List<BO_Field> - Список полей
        private List<BO_Field> _fieldsList = default;
        /// <summary>Список полей</summary>
        public List<BO_Field> FieldsList
        {
            get
            {
                if(_selectedRule is null || _selectedRule.RequiredFields is null)
                {
                    return _fieldsList;
                }

                List<BO_Field> result = new();
                foreach (var item in _fieldsList)
                {
                    if (_selectedRule.RequiredFields.FirstOrDefault(x => x.Name == item.Name) == null)
                        result.Add(item);
                }

                return result;
            }
            set => Set(ref _fieldsList, value);
        }
        #endregion

        #region TemplatesList: List<BO_Template> - Список шаблонов
        private List<BO_Template> _templatesList = default;
        /// <summary>Список шаблонов</summary>
        public List<BO_Template> TemplatesList
        {
            get => _templatesList;
            set => Set(ref _templatesList, value);
        }
        #endregion

        #region SelectedTemplate: BO_Template - Выбранный шаблон
        private BO_Template _selectedTemplate = default;
        /// <summary>Выбранный шаблон</summary>
        public BO_Template SelectedTemplate
        {
            get => _selectedTemplate;
            set
            {
                Set(ref _selectedTemplate, value);
                SelectedRule.Template = value;
            }
        }
        #endregion

        #region DocTypesList: List<string> - Список типов документов
        private List<string> _docTypesList = default;
        /// <summary>Список типов документов</summary>
        public List<string> DocTypesList
        {
            get => _docTypesList;
            set => Set(ref _docTypesList, value);
        }
        #endregion

        #region SelectedDocType: string - Выбранный тип документа
        private string _selectedDocType = "";
        /// <summary>Выбранный тип документа</summary>
        public string SelectedDocType
        {
            get => _selectedDocType;
            set
            {
                Set(ref _selectedDocType, value);
                SelectedRule.DocumentType = value;
            }
        }
        #endregion


        #region SelectedCommand - Выполняется при выделении элемента
        /// <summary>Выполняется при выделении элемента</summary>
        public LambdaCommand SelectedCommand { get; }
        private void OnSelectedCommandExecuted(object sender)
        {
            if (sender is BO_Rule)
            {
                RuleDescription = (sender as BO_Rule).Description;
                OnPropertyChanged(nameof(FieldsList));
            }

            if (sender != null)
            {
                object description = sender.GetType().GetProperty("Description", typeof(string))?.GetValue(sender);

                Description = (string)description ?? "Описание отсутствует";
            }
        }
        #endregion

        #region UpdateRulesList - Команда выполняемая при открытии списка
        /// <summary>Команда выполняемая при открытии списка</summary>
        public LambdaCommand UpdateRulesList { get; }
        private void OnDropDownOpenCommandExecuted()
        {
            RulesList = _rulesService.GetAll().ToList();
        }
        #endregion

        #region UpdateTemplateListCommand - Обновляем список шаблонов
        /// <summary>Обновляем список шаблонов</summary>
        public LambdaCommand UpdateTemplateListCommand { get; }
        private void OnUpdateTemplateListCommandExecuted()
        {
            TemplatesList = _externalObjectsService.GetTAllTemplates().ToList();
            //if (SelectedRule != null)
            //{
            //    if (SelectedRule.Template != null)
            //    {
            //        for (int i = 0; i <= TemplatesList.Count - 1; i++)
            //        {
            //            if (SelectedRule.Template.ID == TemplatesList[i].ID)
            //                SelectedTemplateIndex = i;
            //        }
            //    }
            //    else
            //    {
            //        SelectedTemplateIndex = -1;
            //    }
            //}
        }
        #endregion

        #region UpdateFieldListCommand - Обновляем список полей
        /// <summary>Обновляем список полей</summary>
        public LambdaCommand UpdateFieldListCommand { get; }
        private void OnUpdateFieldListCommandExecuted()
        {
            FieldsList = _externalObjectsService.GetScannerFields().ToList();
        }
        #endregion

        #region UpdateDocTypeCommand - Обновление списка типов документа
        /// <summary>Обновление списка типов документа</summary>
        public LambdaCommand UpdateDocTypeCommand { get; }
        private void OnUpdateDocTypeCommandExecuted()
        {
            List<string> lis = _externalObjectsService.GetAllDocumentTypes().ToList();
            DocTypesList = lis;
        }
        #endregion

        #region NewRule - Создаем новое решение
        /// <summary>Создаем новое решение</summary>
        public LambdaCommand NewRule { get; }
        private void OnNewRuleExecuted()
        {
            SelectedRule = new BO_Rule()
            {
                ID = 0,
                Template = new(),
                DocumentType = "",
                Name = "",
                Description = "",
                RequiredFields = new()
            };
            Description = "";
            RuleDescription = "";
        }
        #endregion

        #region AddFieldToRule - Добавляем поле в решение
        /// <summary>Добавляем поле в решение</summary>
        public LambdaCommand AddFieldToRule { get; }
        private void OnAddFieldToRuleExecuted()
        {
            if (SelectedRule.RequiredFields is null)
                return;
            if (SelectedRule.RequiredFields.FirstOrDefault(f => f.Name == SelectedScanerField.Name) == null)
            {
                SelectedRule.RequiredFields.Add(SelectedScanerField);
                //var tmp = SelectedRule;
                SelectedRule = SelectedRule.Clone();
            }
            OnPropertyChanged(nameof(FieldsList));
        }
        #endregion

        #region SetTemplate - Указать шаблон для решения
        /// <summary>Указать шаблон для решения</summary>
        public LambdaCommand SetTemplate { get; }

        private bool CanSetTemplateExecute()
        {
            if (SelectedRule == null || SelectedRule.Template == null)
                return false;

            if (SelectedRule.Template.ID == SelectedTemplate?.ID)
                return false;

            return true;
        }
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

        private bool CanSetDescriptionExecute()
        {
            if (SelectedRule == null || SelectedRule.Description == null)
                return false;
            if (SelectedRule.Description.Equals(RuleDescription, System.StringComparison.Ordinal))
                return false;

            return true;
        }
        private void OnSetDescriptionExecuted()
        {
            SelectedRule.Description = RuleDescription;
            SelectedRule = SelectedRule.Clone();
        }
        #endregion

        #region SetDocTypeCommand - Устанавливаем тип документа для правила
        /// <summary>Устанавливаем тип документа для правила</summary>
        public LambdaCommand SetDocTypeCommand { get; }

        private bool CanSetDocTypeCommandExecute()
        {
            if (SelectedRule == null || SelectedRule.DocumentType == null)
                return false;
            if (!SelectedRule.DocumentType.Equals(SelectedDocType, System.StringComparison.Ordinal))
                return true;
            return false;
        }
        private void OnSetDocTypeCommandExecuted()
        {
            SelectedRule.DocumentType = SelectedDocType;
            SelectedRule = SelectedRule.Clone();
        }
        #endregion

        #region SaveRule - Сохраняем изменения решения
        /// <summary>Сохраняем изменения решения</summary>
        public LambdaCommand SaveRule { get; }

        private bool CanSaveRuleExecute()
        {
            if (SelectedRule is null)
                return false;
            if (string.IsNullOrWhiteSpace(SelectedRule.Name))
                return false;
            if (SelectedRule.RequiredFields.Count <= 0)
                return false;
            if (SelectedRule.Template == null)
                return false;
            if (SelectedRule.DocumentType == null)
                return false;

            return true;
        }

        private void OnSaveRuleExecuted()
        {
            if (SelectedRule.ID == 0)
                _rulesService.Add(SelectedRule);
            else
                _rulesService.Update(SelectedRule);
        }
        #endregion

        #region DeleteFieldCommand - Удаляем поле из решения
        /// <summary>Удаляем поле из решения</summary>
        public LambdaCommand DeleteFieldCommand { get; }

        private bool CanDeleteFieldCommandExecute()
        {
            if (SelectedField != null && SelectedRule.RequiredFields.FirstOrDefault(f => f.Name == SelectedField.Name) != null)
                return true;
            return false;
        }
        private void OnDeleteFieldCommandExecuted()
        {
            var del = SelectedRule.RequiredFields.FirstOrDefault(f => f.Name == SelectedField.Name);
            SelectedRule.RequiredFields.Remove(del);
            SelectedRule = SelectedRule.Clone();
            OnPropertyChanged(nameof(FieldsList));
        }
        #endregion

        #region DeleteRuleCommand - Удаляем правило
        /// <summary>Удаляем правило</summary>
        public LambdaCommand DeleteRuleCommand { get; }

        private bool CanDeleteRuleCommandExecute()
        {
            if (RulesList == null)
                return false;

            if (SelectedRule != null && RulesList.FirstOrDefault(f => f.ID == SelectedRule.ID) != null)
                return true;

            return false;
        }
        private void OnDeleteRuleCommandExecuted()
        {
            var del = RulesList.FirstOrDefault(f => f.ID == SelectedRule.ID);
            _rulesService.Delete(del);
        }
        #endregion


        public OperatorWindowViewModel(IRulesService rulesService, IExternalObjectsService externalObjectsService)
        {
            #region Инициализация команд
            UpdateRulesList = new(OnDropDownOpenCommandExecuted);
            SelectedCommand = new(OnSelectedCommandExecuted);
            UpdateTemplateListCommand = new(OnUpdateTemplateListCommandExecuted);
            NewRule = new(OnNewRuleExecuted);
            AddFieldToRule = new(OnAddFieldToRuleExecuted);
            SetTemplate = new(OnSetTemplateExecuted, CanSetTemplateExecute);
            SetDescription = new(OnSetDescriptionExecuted, CanSetDescriptionExecute);
            SaveRule = new(OnSaveRuleExecuted, CanSaveRuleExecute);
            UpdateFieldListCommand = new(OnUpdateFieldListCommandExecuted);
            UpdateDocTypeCommand = new(OnUpdateDocTypeCommandExecuted);
            SetDocTypeCommand = new(OnSetDocTypeCommandExecuted, CanSetDocTypeCommandExecute);
            DeleteFieldCommand = new(OnDeleteFieldCommandExecuted, CanDeleteFieldCommandExecute);
            DeleteRuleCommand = new(OnDeleteRuleCommandExecuted, CanDeleteRuleCommandExecute);

            #endregion

            _rulesService = rulesService;
            _externalObjectsService = externalObjectsService;


            RulesList = _rulesService.GetAll().ToList();
            FieldsList = _externalObjectsService.GetScannerFields().ToList();
            TemplatesList = _externalObjectsService.GetTAllTemplates().ToList();
            DocTypesList = _externalObjectsService.GetAllDocumentTypes().ToList();

        }


    }
}
