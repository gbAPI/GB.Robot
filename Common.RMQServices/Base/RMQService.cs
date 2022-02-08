using EasyNetQ;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Common.RMQServices.Base
{
    internal class RMQService
    {
        private string _conection;
        private readonly IBus _busService;

        public string ConectionString => _conection;

        public RMQService()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();

            _conection = config.GetConnectionString("Rabbit");

            _busService = RabbitHutch.CreateBus(_conection);
        }

        internal RMQService Config(string connection)
        {
            _conection = connection;

            return this;
        }

        internal async Task SendMessageAsync<T>(T message, string keyRout, CancellationToken cancellationToken)
            where T : class
        {
            using (var bus = RabbitHutch.CreateBus(_conection))
            {
                await bus.PubSub.PublishAsync(message, keyRout, cancellationToken);
            }
        }

        internal void SendMessage<T>(T message, string keyRout)
            where T : class
        {
            using (var bus = RabbitHutch.CreateBus(_conection))
            {
                bus.PubSub.Publish(message, keyRout);
            }
        }

        internal async Task SubscribesAsync<T>(Action<T> receiverMetod,
            string subscriptionId, string keyRout,
            CancellationToken cancellationToken)
            where T : class
        {
            await _busService.PubSub.SubscribeAsync(subscriptionId, receiverMetod,
                rk => rk.WithTopic(keyRout), cancellationToken);
        }

        internal void Subscribes<T>(Action<T> receiverMetod,
            string subscriptionId, string keyRout)
            where T : class
        {
            _busService.PubSub.Subscribe(subscriptionId, receiverMetod,
                rk => rk.WithTopic(keyRout));
        }
    }
}
