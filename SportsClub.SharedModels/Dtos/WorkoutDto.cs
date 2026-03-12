using FluentValidation;

namespace SportsClub.SharedModels.Dtos;

public class WorkoutDto
{
    public int Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Image { get; set; } = string.Empty;
    public int Duration { get; set; } 
    public decimal? Price { get; init; } 
}

public class WorkoutValidator : AbstractValidator<WorkoutDto>
{
    public WorkoutValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Please enter a workout title");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Please enter a category");
        RuleFor(x => x.Duration).GreaterThan(0).WithMessage("Please enter a duration > 0");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Please enter a valid price");
        //RuleForEach(x => x.Route).SetValidator(new RouteInstructionValidator());
    }
}