using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shabangu.Types.Communicator
{
    public class TextMessage
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Cellnumber { get; set; }
        public string Message { get; set; }
        public string BaseUrl { get; set; }
    }
}
