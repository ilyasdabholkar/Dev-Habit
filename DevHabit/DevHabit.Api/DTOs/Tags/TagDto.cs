

namespace DevHabit.Api.DTOs.Tags;

public sealed record TagCollectionDto
{
    public List<TagDto> Data { get; init; }
}

public class TagDto
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}
