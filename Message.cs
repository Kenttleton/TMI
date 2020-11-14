using System;
using System.Collections.Generic;
using System.Text;

namespace TMI
{
    public class Message
    {
        public string Text { get; set; }
        public string Prefix { get; set; }
        public Command Command { get; set; }
        public string Channel { get; set; }
        

        public Message Parce(string message)
        {
            string[] collection = message.Split(':');

            ParseTags(collection[0]);
            ParseCenterSection(collection[1]);
            this.Text = collection[2];

            return this;
        }

        private void ParseTags(string tagSection)
        {
            char[] delimeters = { '@', '=', ';' };
            var tagParts = tagSection.Split(delimeters);
            var key = "";
            var tags = new Dictionary<string,string>();
            for (var i = 0; i < tagParts.Length - 1; i++)
            {
                if (i % 2 == 0)
                {
                    tags.Add(tagParts[i], "");
                    key = tagParts[i];
                }
                else
                {
                    tags[key] = tagParts[i];
                }
            }
            this.Command.Tags = tags;
        }

        private void ParseCenterSection(string centerSection)
        {
            var centerParts = centerSection.Split(' ');
            this.Prefix = centerParts[0].Trim();
            var success = Enum.TryParse(centerParts[1].Trim(), out CommandList command);
            this.Command.Name = success? command : CommandList.NONE;
            this.Channel = centerParts[2].Trim();
        }

        public override string ToString()
        {
            var tags = BuildTags();
            var centerSection = BuildCenterSection();

            return $"{tags} :{centerSection} :{this.Text}";
        }

        private string BuildTags()
        {
            var tags = "@";
            if (this.Command.Tags.Count == 0) return "";

            foreach (var tag in this.Command.Tags)
            {
                tags = $"{tags}{tag.Key}={tag.Value};";
            }

            tags.TrimEnd(';');
            return $"{tags} ";
        }

        private string BuildCenterSection()
        {
            return $"{this.Prefix} {this.Command} {this.Channel}";
        }
    }

    public class MessageQueue : Queue<Message>{}
    public class RawQueue : Queue<string> { }
}
