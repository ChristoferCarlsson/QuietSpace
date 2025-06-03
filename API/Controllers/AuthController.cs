using API.Services;
using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    private readonly TokenService _tokenService;

    public AuthController(IUserRepository userRepo, TokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email);
        if (user == null || user.Password != dto.Password)
            return Unauthorized("Invalid credentials.");

        var token = _tokenService.CreateToken(user);

        var response = new UserLoginResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = token
        };

        return Ok(response);
    }

    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserCreateDto dto)
    {
        var existing = await _userRepo.GetByEmailAsync(dto.Email);
        if (existing != null)
            return BadRequest("Email already in use.");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password // Reminder: Store hashed password in real apps!
        };

        await _userRepo.AddAsync(user);

        var token = _tokenService.CreateToken(user);

        var response = new UserLoginResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = token
        };

        return Ok(response);
    }
}
