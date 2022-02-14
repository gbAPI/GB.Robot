using Common.RMQServices.Interfaces;
using Common.RMQServices.Models;
using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using Robot.Core;
using Robot.Core.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace GB.Robot.WPF_UI_MVVM.ViewModels
{
    class LoginWindowViewModel : ViewModel
    {
        private readonly Timer _timer;
        private readonly IRobotRabbitService _rabbitService;
        private readonly IProcessingService _processingService;
        private readonly IExternalObjectsService _externalObjects;
        private readonly IQueriesService _queriesService;

        #region Title: string - Название окна
        private string _Title = "Робот-процессор";
        /// <summary>Название окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Tim: string - Время
        private string _tim = default;
        /// <summary>Время</summary>
        public string Tim
        {
            get => _tim;
            set => Set(ref _tim, value);
        }
        #endregion

        public LoginWindowViewModel(IRobotRabbitService rabbitService,
                                    IProcessingService processingService,
                                    IScanerRabbitService scanerRabbit,
                                    IExternalObjectsService externalObjects,
                                    IQueriesService queriesService)
        {
            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
            _rabbitService = rabbitService;
            _processingService = processingService;
            _externalObjects = externalObjects;
            _queriesService = queriesService;
            _rabbitService.SubscribeRobotAsync<DataTransferModel>(OnMessage);

#if false
            Dictionary<string, IEnumerable<string>> idata = new();
            idata.Add("ФИО", new List<string>() { "Фио" });
            idata.Add("Адрес", new List<string>() { "Тулевск" });

            scanerRabbit.SendAsync(new DataTransferModel()
            {
                MessageID = 1,
                DocumentType = "Паспорт",
                InputData = idata,
                PackageProcessed = false
            });
#endif
        }

        private void OnMessage(DataTransferModel data)
        {

            string errDescription = "OK";
            BO_Template templ = null;

            BO_Query query = new()
            {
                DocumentType = data.DocumentType,
                ID = data.MessageID,
                MessageDate = DateTime.Now,
                ErrorDescription = errDescription,
                OutputTemplate = templ
            };

            if (!_processingService.ProcessInputData(data, out int templateId))
            {
                errDescription = "Нет подходящих правил";
                _queriesService.Add(query);
                return;
            }

            templ = _externalObjects.GetTAllTemplates().FirstOrDefault(x => x.ID == templateId);



            _queriesService.Add(query);
            data.OutputTemplateID = templateId;
            _rabbitService.SendToTemplatersAsync(data);


        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) => Tim = DateTime.Now.ToString("G");
    }
}
