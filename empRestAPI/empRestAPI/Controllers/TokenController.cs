using empRestAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace empRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenManager tokenManager;
        public TokenController(ITokenManager _tokenManager)
        {
            tokenManager = _tokenManager;
        }
        [HttpGet]
        public async Task<IActionResult> Auhenticate(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email)) return BadRequest("Email must not be empty");

            return Ok(await tokenManager.GenerateToken(Email));


        }
    }
}
