using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;

namespace API.Controllers
{
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        // GET api/login
        [HttpGet]
        public async Task<string> Connect()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            
            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "user", "password");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api");

            if (tokenResponse.IsError)
            {
                return tokenResponse.Error;
            }

            return tokenResponse.AccessToken;
        }
    }
}
