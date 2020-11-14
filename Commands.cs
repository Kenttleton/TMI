using System;
using System.Collections.Generic;
using System.Text;

namespace TMI
{
    public class Command
    {
        public CommandList Name { get; set; }
        public Dictionary<string, string> Tags { get; set; }
    }

    public enum CommandList
    {
        NONE,
        CLEARCHAT,
        CLEARMSG,
        GLOBALUSERSTATE,
        HOSTTARGET,
        NOTICE,
        PRIVMSG,
        RECONNECT,
        ROOMSTATE,
        USERNOTICE,
        USERSTATE
    }
}
