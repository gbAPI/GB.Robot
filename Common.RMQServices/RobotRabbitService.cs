using Common.RMQServices.Base;
using Common.RMQServices.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Common.RMQServices
{
    /// <summary>
    /// Cервис обмена сообщениями с шиной данных RabbitMQ <b> для робота</b>
    /// </summary>
    public class RobotRabbitService : IRobotRabbitService
    {
        private readonly RMQService _service;

        /// <summary>
        /// Конструктор
        /// </summary>
        public RobotRabbitService()
        {
            _service = new();
        }

        /// <summary>
        /// Асинхронный метод отправляющий данные в шину данных (получатель шаблонизатор)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public async Task SendToTemplatersAsync<T>([NotNull] T message, CancellationToken cancellationToken = default)
            where T : class
        {
            await _service.SendMessageAsync(message, Extensions.Templaters, cancellationToken);
        }

        /// <summary>
        /// Метод отправляющий данные в шину данных (получатель шаблонизатор)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        public void SendToTemplaters<T>([NotNull] T message)
            where T : class
        {
            _service.SendMessage(message, Extensions.Templaters);
        }

        /// <summary>
        /// Асинхронный метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="recivMetod">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param></param>
        /// <returns></returns>
        public async Task SubscribeAsync<T>([NotNull] Action<T> recivMetod, CancellationToken cancellationToken = default)
            where T : class
        {
            await _service.SubscribesAsync(recivMetod, Extensions.RobotProcessor, Extensions.RobotProcessor, cancellationToken);
        }

        /// <summary>
        /// Метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="recivMetod">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        public void Subscribe<T>([NotNull] Action<T> recivMetod)
            where T : class
        {
            _service.Subscribes(recivMetod, Extensions.RobotProcessor, Extensions.RobotProcessor);
        }
    }
}
