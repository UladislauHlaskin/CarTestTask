using CarTestTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;
using CarTestTask.Repository;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace CarTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserRepository _userRepository;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _userRepository = new UserRepository(configuration);
        }
        private bool CheckPassword(User userFromRequest)
        {
            return _userRepository.GetPassword(userFromRequest.UserName) == userFromRequest.Password;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth([FromBody] User user)
        {
            if (CheckPassword(user))
            {
                var tokenString = GenerateJwtToken(user.UserName);
                return Ok(new { Token = tokenString });
            }
            return BadRequest(new {Message = "Incorrect username or password."});
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult CheckAuth()
        {
            return Ok("Authorized");
        }
        /// <summary>
        /// Generate JWT Token after successful login.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userName) }),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
