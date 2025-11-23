using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;

public class UpdateHabitDto
{
    public required string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public required HabitType Type { get; init; }
    public required FrequencyDto Frequency { get; init; }
    public required TargetDto Target { get; init; }
    public DateOnly? EndDate { get; init; }
    public UpdateMilestoneDto? Milestone { get; init; }
}


public sealed class UpdateMilestoneDto
{
    public int Target { get; init; }
}

