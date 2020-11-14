using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMI
{
    public class RedisPubSub
    {
        public const string ChannelName = "TwitchIRC";
        private ConnectionMultiplexer Multiplexer;
        public ISubscriber Subscriber;
        public ISubscriber Publisher;

        public RedisPubSub(string config)
        {
            config = String.IsNullOrWhiteSpace(config) ? "redis:6379" : config;
            Configure(config);
        }

        private void Configure(string config)
        {
            var configuration = ConfigurationOptions.Parse(config);
            configuration.Password = "sOmE_sEcUrE_pAsS";
            this.Multiplexer = ConnectionMultiplexer.Connect(configuration);
            var database = this.Multiplexer.GetDatabase();
            this.Subscriber = this.Multiplexer.GetSubscriber();
            this.Publisher = this.Multiplexer.GetSubscriber();
        }
    }
}
