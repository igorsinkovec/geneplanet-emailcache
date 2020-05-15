using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailCache.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly IClusterClient _client;

        public EmailsController(IClusterClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new [] {
                "fu",
                "bar"
            };
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<bool>> Exists(string email)
        {
            var emailList = _client.GetGrain<IEmailList>(getDomain(email));
            var response = await emailList.Contains(email);
            Console.WriteLine("\n EmailExists: {0}\n", response);
            return response;
        }

        private static string getDomain(string email)
        {
            return new MailAddress(email).Host;
        }

        private static string getTopLevelDomain(string email)
        {
            return getDomain(email).Split('.')[1];
        }
    }
}
