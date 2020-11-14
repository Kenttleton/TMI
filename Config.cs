using System;
using System.Collections.Generic;
using System.Text;

namespace TMI
{
    public class Config
    {
        public const string TAuth = "id.twitch.tv";
        public const string TMIPrefix = "tmi.twitch.tv";
        public const string IRCEndpoint = "irc.chat.twitch.tv";
        public const int IRCPort = 6667;
        public byte[] ReadBuffer = new byte[256];
        public byte[] WriteBuffer = new byte[256];
        public Encoding MessageEncoding = Encoding.ASCII;
        public Identity Identity { get; set; }
        public List<string> Channels = new List<string>() { "#tmijs" };
        public Config(Identity identity, string channel)
        {
            Channels.Add(channel);
            Identity = identity;
        }
    }
}
