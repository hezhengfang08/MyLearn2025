﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.AuthServer.Services;

namespace MySelf.MSACommerce.AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(IIdentityService identityService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string username, string password)
        {
            var result = await identityService.GetAccessTokenAsync(username, password);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { errors = result.Errors });
        }
    }
}
