namespace SportsClub.SharedModels.Dtos;

public record LoginRequest(
    string Username,
    string Password);

public record TokenResponse(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn);

public record LoginResponse(
    int UserId,
    string Username,
    string AccessToken,
    string RefreshToken,
    int ExpiresIn);

public record RefreshTokenRequest(
    string RefreshToken);
