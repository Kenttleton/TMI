using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace TMI
{
    public class Client: TcpClient
    {
        private Config Config;
        private StreamReader Reader;
        private StreamWriter Writer;
        private NetworkStream Stream;

        public Client Build(Config config)
        {
            Config = config;
            Config.Channels.Add($"#{Config.Identity.Username}");
            return this;
        }

        public void Run(string[] args)
        {
            if (String.IsNullOrWhiteSpace(Config.Identity.Username) && String.IsNullOrWhiteSpace(Config.Identity.Token)) return;

            Console.WriteLine($"Connecting to Twitch.tv chat");
            Connect(Config.IRCEndpoint, Config.IRCPort);
            Console.WriteLine($"Connected: {Connected}");
            Stream = GetStream();
            Writer = new StreamWriter(Stream);
            Reader = new StreamReader(Stream);
            Authenticate();
            ApplyMember(Config.Identity.Username);
            JoinChannel(Config.Channels[1]);

            while (Connected)
            {
                //if (Console.In.Peek() != -1 && Console.KeyAvailable)
                if (Console.KeyAvailable)
                {
                    var lineToSend = Console.ReadLine();
                    var input = InputParse(lineToSend);

                    if (input != null)
                    {
                        Writer.WriteLine(input);
                        Writer.Flush();
                    }
                }

                if (Stream.DataAvailable)
                {
                    var message = Reader.ReadLine();
                    Console.WriteLine(message);

                    if (message == "PING")
                    {
                        Writer.WriteLine("PONG");
                        Writer.Flush();
                    }
                }
            }
        }

        private void Authenticate()
        {
            Writer.WriteLine($"PASS {Config.Identity.Token}");
            Writer.Flush();
        }

        private void JoinChannel(string channelName)
        {
            Writer.WriteLine($"JOIN {channelName}");
            Writer.Flush();
        }

        private void LeaveChannel(string channelName)
        {
            Writer.WriteLine($"PART {channelName}");
            Writer.Flush();
        }

        private void ApplyMember(string member)
        {
            Writer.WriteLine($"NICK {member}");
            Writer.Flush();
        }

        public string InputParse(string p)
        {
            var privmsg = $":{Config.Identity.Username}!{Config.Identity.Username}@{Config.Identity.Username}.{Config.TMIPrefix} {CommandList.PRIVMSG.ToString()} {Config.Channels[1]} :";
            var cliCollection = p.Split(' ');
            var flag = "";
            var subp = "";
            Regex flagMatch = new Regex("^-[a-z]$");

            foreach (string section in cliCollection)
            {
                if (flagMatch.IsMatch(section))
                {
                    flag = section;
                }
                else
                {
                    subp = subp + section + " ";
                }
            }

            switch (flag)
            {
                case "-m":
                    return $"{privmsg}{subp.Trim()}";
                case "-p":
                    return "PING";
                default:
                    return null;
            }
        }
    }
}