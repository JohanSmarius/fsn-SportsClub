using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClub.Api.Data;
using SportsClub.Api.Entities;
using SportsClub.Api.Services;
using SportsClub.SharedModels.Dtos;
using System.Security.Cryptography;
using System.Text;

namespace SportsClub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SportsClubDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(SportsClubDbContext context, IJwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Username and password are required");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid credentials");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        // Save refresh token to database
        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(2),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync();

        return Ok(new LoginResponse(
            UserId: user.Id,
            Username: user.Username,
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresIn: 900 // 15 minutes in seconds
        ));
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenResponse>> Refresh([FromBody] RefreshTokenRequest request)
    {
        if (string.IsNullOrEmpty(request.RefreshToken))
        {
            return BadRequest("Refresh token is required");
        }

        var refreshToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && !rt.IsRevoked);

        if (refreshToken == null || refreshToken.ExpiryDate < DateTime.UtcNow)
        {
            return Unauthorized("Invalid or expired refresh token");
        }

        var user = refreshToken.User;
        if (user == null)
        {
            return Unauthorized("User not found");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

        // Revoke old refresh token and create new one
        refreshToken.IsRevoked = true;
        var newRefreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(2),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };

        _context.RefreshTokens.Add(newRefreshTokenEntity);
        await _context.SaveChangesAsync();

        return Ok(new TokenResponse(
            AccessToken: accessToken,
            RefreshToken: newRefreshToken,
            ExpiresIn: 900 // 15 minutes in seconds
        ));
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == hash;
    }
}
