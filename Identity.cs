using System;
using System.Collections.Generic;
using System.Text;

namespace TMI
{
    public class Identity
    {
        private string _username { get; set; }
        public string Username { get { return _username; } set { _username = value; } }
        private string _token { get; set; }
        public string Token
        {
            get
            {
                return $"oauth:{_token}";
            }
            set
            {
                _token = value.ToLowerInvariant().Replace("oauth:", "");
            }
        }
    }
}
