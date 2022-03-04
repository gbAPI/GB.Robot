using Common.RMQServices.Interfaces;
using Common.RMQServices.Models;
using GB.Robot.WPF_UI_MVVM.ViewModels.Base;
using Robot.Core;
using Robot.Core.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public LoginWindowViewModel(IProcessingService processingService,
                                    IRobotRabbitService rabbitService,
                                    IScanerRabbitService scanerRabbit,
                                    IExternalObjectsService externalObjects,
                                    IQueriesService queriesService)
        {
            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();

            _processingService = processingService;
            _externalObjects = externalObjects;
            _queriesService = queriesService;

            _rabbitService = rabbitService;
            _rabbitService.SubscribeRobotAsync<DataTransferModel>(OnMessage);
        }

        private void OnMessage(DataTransferModel data)
        {
            BO_Query query = new()
            {
                DocumentType = data.DocumentType,
                ID = data.MessageID,
                MessageDate = DateTime.Now,
                ErrorDescription = "OK",
                OutputTemplate = null
            };

            if (!_processingService.ProcessInputData(data, out int templateId))
            {
                query.ErrorDescription = "Нет подходящих правил";
                _queriesService.Add(query);
                return;
            }

            query.OutputTemplate = _externalObjects.GetTAllTemplates().FirstOrDefault(x => x.ID == templateId);

            _queriesService.Add(query);
            data.OutputTemplateID = templateId;
            //_rabbitService.SendToTemplaters(data);
            Task.Run(async ()=> await _rabbitService.SendToTemplatersAsync(data)).RunSynchronously();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) => Tim = DateTime.Now.ToString("G");
    }
}
