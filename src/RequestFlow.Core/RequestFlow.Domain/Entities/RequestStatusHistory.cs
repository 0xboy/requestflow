using RequestFlow.Shared.Constants;

namespace RequestFlow.Domain.Entities;

public class RequestStatusHistory
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public RequestStatus FromStatus { get; set; }
    public RequestStatus ToStatus { get; set; }
    public string? Comment { get; set; }
    public string ChangedByUserId { get; set; } = string.Empty;
    public DateTime ChangedDate { get; set; }

    public Request Request { get; set; } = null!;
}
