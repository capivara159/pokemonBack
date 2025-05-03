using Microsoft.AspNetCore.Mvc;
using PokemonApi.Models.Auth;
using PokemonApi.Services;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly TokenService _tokenService;

    public AuthController(AuthService authService, TokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _authService.Register(request.Email, request.Password);
        if (user == null)
            return BadRequest("Usuário já existe");

        var token = _tokenService.GenerateToken(user);
        return Ok(new LoginResponse { Token = token });
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _authService.Login(request.Email, request.Password);
        if (user == null)
            return Unauthorized("Credenciais inválidas");

        var token = _tokenService.GenerateToken(user);
        return Ok(new LoginResponse { Token = token });
    }
}