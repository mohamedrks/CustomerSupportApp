using ChatAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Interfaces
{
    public interface IConsumerService
    {
        Task ReadMessgaes();
        Task SendMessgaes(ClientUser user);
    }
}
