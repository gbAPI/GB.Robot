using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Common.RMQServices.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса обмена сообщениями с шиной данных RabbitMQ <b> для робота</b>
    /// </summary>
    public interface IRobotRabbitService
    {
        /// <summary>
        /// Асинхронный метод отправляющий данные в шину данных (получатель шаблонизатор)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public Task SendToTemplatersAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Метод отправляющий данные в шину данных (получатель шаблонизатор)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        public void SendToTemplaters<T>(T message) where T : class;


        /// <summary>
        /// Асинхронный метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="recivMetod">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public Task SubscribeRobotAsync<T>(Action<T> recivMetod,
                                      CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="recivMetod">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        public void SubscribeRobot<T>(Action<T> recivMetod) where T : class;
    }
}
