using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Models
{
    public class MessageDto
    {
        public string User { get; set; }
        public string ConnectionId { get; set; }
        public string MsgText { get; set; }
    }
}
