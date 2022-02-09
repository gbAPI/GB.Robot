using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.RMQServices.Interfaces
{
    public interface IRMQServiceBase
    {
        /// <summary>
        /// Асинхронный метод отправляющий данные в шину данных
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="body">Сообщение</param>
        /// <param name="keyRout">Ключ маршрута сообщения</param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public Task SendMessageAsync<T>(T body, string keyRout, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Асинхронный метод отправляющий данные в шину данных
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="body">Сообщение</param>
        /// <param name="exchangeName">Имя обменика для приема сообщения</param>
        /// <param name="keyRout">Ключ маршрута сообщения</param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public Task SendMessageAsync<T>(T body,
                                                string exchangeName,
                                                string keyRout,
                                                CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Метод отправляющий данные в шину данных
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="body">Сообщение</param>
        /// <param name="keyRout">Ключ маршрута сообщения</param>
        public void SendMessage<T>(T body, string keyRout) where T : class;

        /// <summary>
        /// Метод отправляющий данные в шину данных
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="body">Сообщение</param>
        /// <param name="exchangeName">Имя обменика для приема сообщения</param>
        /// <param name="keyRout">Ключ маршрута сообщения</param>
        public void SendMessage<T>(T body, string exchangeName, string keyRout) where T : class;


        /// <summary>
        /// Асинхронный метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="exchangeName">Имя обменика для приема сообщения</param>
        /// <param name="subscribeID">ID подписчика</param>
        /// <param name="action">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        /// <param name="keyRouting">Ключ маршрута сообщения</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SubscribesAsync<T>(Action<T> action,
                                             string exchangeName,
                                             string subscribeID,
                                             string keyRouting,
                                             CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Асинхронный метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="subscribeID">ID подписчика</param>
        /// <param name="action">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        /// <param name="keyRouting">Ключ маршрута сообщения</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SubscribesAsync<T>(Action<T> action,
                                             string subscribeID,
                                             string keyRouting,
                                             CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="exchangeName">Имя обменика для приема сообщения</param>
        /// <param name="subscribeID">ID подписчика</param>
        /// <param name="action">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        /// <param name="keyRouting">Ключ маршрута сообщения</param>
        public void Subscribes<T>(Action<T> action,
                                  string exchangeName,
                                  string subscribeID,
                                  string keyRouting) where T : class;

        /// <summary>
        /// Метод подписки на входящие сообщения
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="subscribeID">ID подписчика</param>
        /// <param name="action">Метод обрабатывающий получающий в качестве параметра сообщения
        /// <i>Вид void Metod(T message){}</i></param>
        /// <param name="keyRouting">Ключ маршрута сообщения</param>
        public void Subscribes<T>(Action<T> action,
                                 string subscribeID,
                                 string keyRouting) where T : class;
    }
}
