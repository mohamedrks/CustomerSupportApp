using ChatAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSupportController : ControllerBase
    {
        IConsumerService _consumerService;

        public CustomerSupportController(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        [HttpGet]
        public async Task<string> Get(string message, string sessionName)
        {

            await _consumerService.SendMessgaes(new Models.ClientUser() { MsgText = message , User = sessionName});
            return "OK";
        }


        [HttpGet("GetMessages")]
        public async Task<string> GetMessages()
        {

            await _consumerService.ReadMessgaes();
            return "OK";
        }
    }
}
