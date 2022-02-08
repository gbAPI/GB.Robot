using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Common.RMQServices.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса обмена сообщениями с шиной данных RabbitMQ <b> для шаблонизатора</b>
    /// </summary>
    public interface ITemplateRabbitService
    {
        /// <summary>
        /// Асинхронный метод отправляющий данные в шину данных (получатель робот)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public Task SendAsync<T>([NotNull] T message, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Метод отправляющий данные в шину данных (получатель робот)
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="message">Само сообщение</param>
        public void Send<T>([NotNull] T message) where T : class;

        /// <summary>
        /// Асинхронный метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="recivMetod">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param></param>
        /// <returns></returns>
        public Task SubscribeAsync<T>([NotNull] Action<T> recivMetod, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="recivMetod">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        public void Subscribe<T>([NotNull] Action<T> recivMetod) where T : class;
    }
}
