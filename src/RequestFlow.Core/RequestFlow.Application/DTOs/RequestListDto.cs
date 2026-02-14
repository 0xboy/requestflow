using RequestFlow.Shared.Constants;

namespace RequestFlow.Application.DTOs;

public record RequestListDto
{
    public int Id { get; init; }
    public string RequestNo { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public RequestStatus Status { get; init; }
    public Priority Priority { get; init; }
    public DateTime CreatedDate { get; init; }
}
