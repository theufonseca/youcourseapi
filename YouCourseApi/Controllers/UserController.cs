using Application.UseCases.UserUseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YouCourseApi.Models;

namespace YouCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly TokenConfiguration tokenConfig;

        public UserController(IMediator mediator, TokenConfiguration tokenConfig)
        {
            this.mediator = mediator;
            this.tokenConfig = tokenConfig;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] NewUserRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var response = await mediator.Send(request);

            if(response.user is not null)
            {
                var authClaims = new List<Claim>
                {
                    new (ClaimTypes.Name, response.user.Name),
                    new (ClaimTypes.Email, response.user.Email),
                    new (ClaimTypes.Sid, response.user.Id.ToString())
                };

                var token = GetToken(authClaims);
                return Ok(token);
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var response = await mediator.Send(new GetActiveUserRequest(id));
            return Ok(response);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserInfoRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await mediator.Send(new DeleteUserRequest(id));
            return Ok(response);
        }
        
        private TokenModel GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.SecretJwtKey!));

            var token = new JwtSecurityToken(
                issuer: tokenConfig.Issuer,
                audience: tokenConfig.Audience,
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
