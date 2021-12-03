using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAPI.Models
{
    public class ClientUser
    {
        public string User { get; set; }
        public string ConnectionId { get; set; }
        public string MsgText { get; set; }
    }
}
