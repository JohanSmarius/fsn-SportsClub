namespace SportsClub.SharedModels.Dtos;

public record WorkoutDto(
    int Id,
    string Title = "",
    string Description = "",
    string Category = "",
    string? Image = null,
    int Duration = 0,
    decimal? Price = null);
