using Common.RMQServices.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.RMQServices
{
    public class RobotRabbitClient : IRobotRabbitService
    {
        const string queue = "Robot_IN";
        const string exchange = "gb.exchange";
        ConnectionFactory _factory;
        IModel _channel;
        IConnection connection;
        EventingBasicConsumer consumer;
        Action<object> _subs;
        public RobotRabbitClient()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "b-cf498a7e-3340-4659-ac36-9b1fa6ad3717.mq.us-east-1.amazonaws.com",
                Port = 5671,
                UserName = "admin",
                Password = "admin1234567"
            };
            Uri url = new Uri("amqps://b-cf498a7e-3340-4659-ac36-9b1fa6ad3717.mq.us-east-1.amazonaws.com:5671");
            _factory.Ssl.Enabled = true;
            _factory.Uri = url;
            connection = _factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void SendToTemplaters<T>(T message) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task SendToTemplatersAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            var mes = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(mes);
            _channel.BasicPublish("DataTransferModel", "temp",body: body);
        }

        public void SubscribeRobot<T>(Action<T> recivMetod) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task SubscribeRobotAsync<T>(Action<T> recivMetod, CancellationToken cancellationToken = default) where T : class
        {
            _channel.QueueDeclare(queue,true,false,false);
            _channel.ExchangeDeclare(exchange,ExchangeType.Direct,true);
            _channel.QueueBind(queue, exchange, "robo");
            consumer = new EventingBasicConsumer(_channel);
            _subs = new(o => recivMetod((T)o));
            consumer.Received += Consumer_Received<T>;
            _channel.BasicConsume(queue,true,consumer);
        }

        private void Consumer_Received<T>(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            T mesBody = JsonConvert.DeserializeObject<T>(message);
            _subs?.Invoke(mesBody);
        }
    }
}
