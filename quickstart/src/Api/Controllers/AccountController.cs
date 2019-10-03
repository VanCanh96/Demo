using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountContext _accountContext;

        public AccountController(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return new JsonResult(_accountContext.Account.ToList());
        }

        [HttpGet("add")]
        public async Task<IActionResult> Add(string username = "admin", string password = "pass")
        {
            _accountContext.Account.Add(new Models.Account
            {
                Name = username,
                UserName = username,
                Password = password
            });

            await _accountContext.SaveChangesAsync();
            return new JsonResult(_accountContext.Account.ToList());
        }
    }
}