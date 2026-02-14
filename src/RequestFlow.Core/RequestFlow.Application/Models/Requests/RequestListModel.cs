using RequestFlow.Shared.Constants;

namespace RequestFlow.Application.Models.Requests;

/// <summary>
/// Request listesi öğesi için application modeli. DTO'ya map edilir.
/// </summary>
public record RequestListModel
{
    public int Id { get; init; }
    public string RequestNo { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public RequestStatus Status { get; init; }
    public Priority Priority { get; init; }
    public DateTime CreatedDate { get; init; }
}
