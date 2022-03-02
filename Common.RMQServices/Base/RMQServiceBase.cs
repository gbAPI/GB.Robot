using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.RMQServices.Base
{
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    public class RMQServiceBase
    {
        private readonly string _conection;
        private readonly IBus _busService;
        private readonly IAdvancedBus _busAdv;
        private readonly Dictionary<string, Action<object>> _subs;


        public RMQServiceBase()
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();

            _conection = config.GetConnectionString("Rabbit");

            _busService = RabbitHutch.CreateBus(_conection);
            _busAdv = _busService.Advanced;

            _subs = new();
        }



        #region Отправка сообщений
        /// <summary>
        /// Асинхронный метод отправляющий данные в шину данных
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="body">Сообщение</param>
        /// <param name="keyRout">Ключ маршрута сообщения</param>
        /// <param name="cancellationToken"><i>Необязательный параметр.</i>
        /// Распространяет уведомление о том, что операции следует отменить.</param>
        /// <returns></returns>
        public async Task SendMessageAsync<T>(T body, string keyRout, CancellationToken cancellationToken = default)
            where T : class
        {
            await SendMessageAsync(body, CreateName<T>(), keyRout, cancellationToken);
        }

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
        public async Task SendMessageAsync<T>(T body,
                                                string exchangeName,
                                                string keyRout,
                                                CancellationToken cancellationToken = default) where T : class
        {
            #region Проверки
            if (body is null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            if (string.IsNullOrWhiteSpace(exchangeName))
            {
                throw new ArgumentException($"\"{nameof(exchangeName)}\" не может быть пустым или содержать только пробел.", nameof(exchangeName));
            }

            if (string.IsNullOrWhiteSpace(keyRout))
            {
                throw new ArgumentException($"\"{nameof(keyRout)}\" не может быть пустым или содержать только пробел.", nameof(keyRout));
            }

            #endregion



            var exchange = await _busAdv.ExchangeDeclareAsync(exchangeName,
                                                          ExchangeType.Topic,
                                                          cancellationToken: cancellationToken);

            Message<T> message = new(body);

            await _busAdv.PublishAsync(exchange, keyRout, false, message, cancellationToken);

        }

        /// <summary>
        /// Метод отправляющий данные в шину данных
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="body">Сообщение</param>
        /// <param name="keyRout">Ключ маршрута сообщения</param>
        public void SendMessage<T>(T body, string keyRout)
            where T : class
        {
            SendMessage(body, CreateName<T>(), keyRout);
        }

        /// <summary>
        /// Метод отправляющий данные в шину данных
        /// </summary>
        /// <typeparam name="T">Тип сообщения (обязательно класс)</typeparam>
        /// <param name="body">Сообщение</param>
        /// <param name="exchangeName">Имя обменика для приема сообщения</param>
        /// <param name="keyRout">Ключ маршрута сообщения</param>
        public void SendMessage<T>(T body, string exchangeName, string keyRout)
            where T : class
        {
            #region Проверки
            if (body is null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            if (string.IsNullOrWhiteSpace(exchangeName))
            {
                throw new ArgumentException($"\"{nameof(exchangeName)}\" не может быть пустым или содержать только пробел.", nameof(exchangeName));
            }

            if (string.IsNullOrWhiteSpace(keyRout))
            {
                throw new ArgumentException($"\"{nameof(keyRout)}\" не может быть пустым или содержать только пробел.", nameof(keyRout));
            }

            #endregion


            var exchange = _busAdv.ExchangeDeclare(exchangeName, ExchangeType.Topic);

            Message<T> message = new(body);

            _busAdv.PublishAsync(exchange, keyRout, false, message);

        }

        #endregion


        #region Подписка
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
        public async Task SubscribesAsync<T>(Action<T> action,
                                             string exchangeName,
                                             string subscribeID,
                                             string keyRouting,
                                             CancellationToken cancellationToken = default) where T : class
        {
            #region Проверки
            if (string.IsNullOrWhiteSpace(exchangeName))
            {
                throw new ArgumentException($"\"{nameof(exchangeName)}\" не может быть пустым или содержать только пробел.", nameof(exchangeName));
            }

            if (string.IsNullOrWhiteSpace(subscribeID))
            {
                throw new ArgumentException($"\"{nameof(subscribeID)}\" не может быть пустым или содержать только пробел.", nameof(subscribeID));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (string.IsNullOrWhiteSpace(keyRouting))
            {
                throw new ArgumentException($"\"{nameof(keyRouting)}\" не может быть пустым или содержать только пробел.", nameof(keyRouting));
            }

            #endregion

            Action<object> ac = new(o => action((T)o));
            if (_subs.ContainsKey(keyRouting))
            {
                _subs[keyRouting] += ac;
            }
            else
            {
                _subs.Add(keyRouting, ac);
            }

            var queue = await _busAdv.QueueDeclareAsync($"{exchangeName}_{subscribeID}", cancellationToken);
            var exchange = await _busAdv.ExchangeDeclareAsync(exchangeName,
                                                          ExchangeType.Topic,
                                                          cancellationToken: cancellationToken);
            var binding = await _busAdv.BindAsync(exchange, queue, keyRouting, cancellationToken);
            _busAdv.Consume(queue, OnMessage<T>);
        }

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
        public async Task SubscribesAsync<T>(Action<T> action,
                                             string subscribeID,
                                             string keyRouting,
                                             CancellationToken cancellationToken = default) where T : class
        {
            await SubscribesAsync(action, CreateName<T>(), subscribeID, keyRouting, cancellationToken);
        }


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
                                  string keyRouting) where T : class
        {
            #region Проверки
            if (string.IsNullOrWhiteSpace(exchangeName))
            {
                throw new ArgumentException($"\"{nameof(exchangeName)}\" не может быть пустым или содержать только пробел.", nameof(exchangeName));
            }

            if (string.IsNullOrWhiteSpace(subscribeID))
            {
                throw new ArgumentException($"\"{nameof(subscribeID)}\" не может быть пустым или содержать только пробел.", nameof(subscribeID));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (string.IsNullOrWhiteSpace(keyRouting))
            {
                throw new ArgumentException($"\"{nameof(keyRouting)}\" не может быть пустым или содержать только пробел.", nameof(keyRouting));
            }

            #endregion

            Action<object> ac = new(o => action((T)o));
            if (_subs.ContainsKey(keyRouting))
            {
                _subs[keyRouting] += ac;
            }
            else
            {
                _subs.Add(keyRouting, ac);
            }

            var adv = _busService.Advanced;
            var queue = _busAdv.QueueDeclare($"{exchangeName}_{subscribeID}");
            var exchange = _busAdv.ExchangeDeclare(exchangeName, ExchangeType.Topic);
            var binding = _busAdv.Bind(exchange, queue, keyRouting);
            _busAdv.Consume(queue, OnMessage<T>);
        }

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
                                 string keyRouting) where T : class
        {
            Subscribes(action, CreateName<T>(), subscribeID, keyRouting);
        }

        #endregion

        private void OnMessage<T>(byte[] body, MessageProperties properties, MessageReceivedInfo receivedInfo) where T : class
        {
            string rk = receivedInfo.RoutingKey;
            T message = System.Text.Json.JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));

            if (rk == "#")
            {
                foreach (var item in _subs)
                {
                    item.Value?.Invoke(message);
                }
                return;
            }

            if (_subs.ContainsKey("#"))
            {
                _subs["#"]?.Invoke(message);
            }

            if (_subs.ContainsKey(rk))
            {
                _subs[rk]?.Invoke(message);
                return;
            }

            if (!_subs.ContainsKey("#") && !_subs.ContainsKey(rk))
            {
                throw new NotSupportedException();
            }
        }

        private static string CreateName<T>(string postFix = null, bool isDlx = false)
        {
            string tmessage = typeof(T).Name;
            if (!string.IsNullOrWhiteSpace(postFix))
                tmessage += $"_{postFix}";

            if (isDlx)
            {
                tmessage += "_dlx";
            }
            return tmessage;
        }
    }
}
