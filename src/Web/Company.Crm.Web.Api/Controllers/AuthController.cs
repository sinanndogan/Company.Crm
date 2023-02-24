using Company.Crm.Application.Dtos;
using Company.Crm.Application.Dtos.Auth;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Usr;
using Company.Framework.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IEmployeeService _employeeService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService, IConfiguration configuration, IEmployeeService employeeService)
    {
        _userService = userService;
        _configuration = configuration;
        _employeeService = employeeService;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(LoginDto model)
    {
        if (String.IsNullOrEmpty(model.EmailAddressOrUsername) || String.IsNullOrEmpty(model.Password))
            return BadRequest("Invalid user");

        var user = _userService.Login(model);

        if (user != null)
        {
            var token = await GetJwtToken(user);

            return Ok(token);
        }

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerModel)
    {
        if (String.IsNullOrEmpty(registerModel.EmailAddress))
            return BadRequest("User is exist!");

        var isUserExist = await _userService.IsUserExist(registerModel.EmailAddress, registerModel.EmailAddress);

        if (isUserExist)
            return BadRequest("User is exist!");

        var user = _userService.Register(registerModel);
        if (user != null)
        {
            user.Password = "";

            return Ok(user);
        }

        return BadRequest("User is exist!");
    }

    [HttpPost("me")]
    public IActionResult Me()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (userId == 0) return BadRequest("User not found!");

        var employeeResponse = _employeeService.GetByUserId(userId);
        if (!employeeResponse.IsSuccess || employeeResponse.Data == null) return BadRequest("Employee not found!");

        var data = employeeResponse.Data;
        var genderLetter = data.GenderId == 1 ? "m" : "f";

        return Ok(new SessionUserDto
        {
            FullName = data.UserFullName,
            Title = data.TitleName,
            ProfilePhoto = $"00{data.UserId}{genderLetter}.jpg"
        });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> AuthenticateByRefreshToken([FromBody] string refreshToken)
    {
        if (refreshToken == null) return BadRequest();

        var refreshTokenUser = await _userService.GetUserByRefreshToken(refreshToken);
        if (refreshTokenUser == null) return Unauthorized("No user!");

        if (refreshTokenUser.RefreshToken == null || refreshTokenUser.RefreshToken != refreshToken)
            return Unauthorized("Invalid refresh token!");

        if (refreshTokenUser.RefreshTokenExpireDate < DateTime.Now)
            return Unauthorized("Token expired!");

        var user = _userService.GetById(refreshTokenUser.Id);
        var token = await GetJwtToken(user);

        return Ok(token);
    }

    private async Task<JwtToken> GetJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name + " " + user.Surname),
            new(ClaimTypes.GivenName, user.Name),
            new(ClaimTypes.Surname, user.Surname),
            new(ClaimTypes.Email, user.Email),
            //new Claim("Permissions", "1,2")
        };

        if (user.Roles != null && user.Roles.Any())
        {
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Jwt:SecurityKey"]));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddHours(6);
        var tokenOptions = new JwtSecurityToken(
            issuer: _configuration["Auth:Jwt:Issuer"],
            audience: _configuration["Auth:Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        var refreshToken = SecurityHelper.CreateToken();
        await _userService.UpdateRefreshToken(user.Id, refreshToken, DateTime.Now.AddYears(1));

        return new JwtToken(accessToken, refreshToken, expires);
    }
}