using Common.RMQServices.Base;
using Common.RMQServices.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RMQMessageServices
{
    /// <summary>
    /// Cервис обмена сообщениями с шиной данных RabbitMQ <b> для робота</b>
    /// </summary>
    public class RobotRabbitService : RMQServiceBase, IRobotRabbitService
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public RobotRabbitService()
        {
        }

        /// <summary>
        /// Асинхронный метод отправляющий данные в шину данных (получатель шаблонизатор)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public async Task SendToTemplatersAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            await SendMessageAsync(message, Extensions.Templaters, cancellationToken);
        }

        /// <summary>
        /// Метод отправляющий данные в шину данных (получатель шаблонизатор)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        public void SendToTemplaters<T>(T message) where T : class
        {
            SendMessage(message, Extensions.Templaters);
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
        public async Task SubscribeRobotAsync<T>(Action<T> recivMetod,
                                            CancellationToken cancellationToken = default) where T : class
        {
            await SubscribesAsync<T>(recivMetod, Extensions.RobotProcessor, Extensions.RobotProcessor, cancellationToken);
        }

        /// <summary>
        /// Метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="recivMetod">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        public void SubscribeRobot<T>(Action<T> recivMetod) where T : class
        {
            Subscribes(recivMetod, Extensions.RobotProcessor, Extensions.RobotProcessor);
        }

    }
}
