using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopApp.API.Dtos;
using ShopApp.API.Models;
using ShopApp.API.Static;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ShopApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly IConfiguration configuration;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            logger.LogInformation($"Registration attempt for {userDto.Email}");
            try
            {
                var user = mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(error.Code, error.Description);
                    return BadRequest(ModelState);
                }

                await userManager.AddToRoleAsync(user, "User");
                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured at method {nameof(Register)}");
                return Problem(Messages.Error500, statusCode : 500);
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(UserLoginDto userLoginDto)
        {
            logger.LogInformation($"Login attempt for {userLoginDto.Email}");
            try
            {
                var user = await userManager.FindByEmailAsync(userLoginDto.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, userLoginDto.Password);
                if (user == null || !passwordValid) return Unauthorized(userLoginDto);

                var tokenString = await GenerateToken(user);

                var response = new AuthResponse
                {
                    Email = userLoginDto.Email,
                    Token = tokenString,
                    userId = user.Id,
                };

                return Accepted(response);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured at method {nameof(Login)}");
                return Problem(Messages.Error500, statusCode : 500);
            }
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt16(configuration["JwtSettings:Duration"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
