using Common.RMQServices.Base;
using Common.RMQServices.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.RMQServices
{
    /// <summary>
    /// Cервис обмена сообщениями с шиной данных RabbitMQ <b> для сканировщика</b>
    /// </summary>
    public class ScanerRabbitService : RMQServiceBase, IScanerRabbitService
    {

        /// <summary>
        /// Конструктор
        /// </summary>
        public ScanerRabbitService()
        {
        }

        /// <summary>
        /// Асинхронный метод отправляющий данные в шину данных (получатель робот)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public async Task SendAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : class
        {
            await SendMessageAsync(message, Extensions.RobotProcessor, cancellationToken);
        }

        /// <summary>
        /// Метод отправляющий данные в шину данных (получатель робот)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        public void Send<T>(T message)
            where T : class
        {
            SendMessage(message, Extensions.RobotProcessor);
        }

        /// <summary>
        /// Асинхронный метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="recivMetod">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public async Task SubscribeScanerAsync<T>(Action<T> recivMetod, CancellationToken cancellationToken = default)
            where T : class
        {
            await SubscribesAsync(recivMetod, Extensions.Scaners, Extensions.Scaners, cancellationToken);
        }

        /// <summary>
        /// Метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="recivMetod">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        public void SubscribeScaner<T>(Action<T> recivMetod)
            where T : class
        {
            Subscribes(recivMetod, Extensions.Scaners, Extensions.Scaners);
        }
    }
}
