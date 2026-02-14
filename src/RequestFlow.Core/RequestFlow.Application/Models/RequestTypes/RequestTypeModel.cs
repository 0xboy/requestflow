namespace RequestFlow.Application.Models.RequestTypes;

/// <summary>
/// Application model for request type. Mapped to DTO.
/// </summary>
public record RequestTypeModel
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
