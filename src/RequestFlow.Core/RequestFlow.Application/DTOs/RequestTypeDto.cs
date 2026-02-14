namespace RequestFlow.Application.DTOs;

public record RequestTypeDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
