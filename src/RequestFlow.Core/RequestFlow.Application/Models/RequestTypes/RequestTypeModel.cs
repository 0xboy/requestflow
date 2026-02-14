namespace RequestFlow.Application.Models.RequestTypes;

/// <summary>
/// Request tipi için application modeli. DTO'ya map edilir.
/// </summary>
public record RequestTypeModel
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
