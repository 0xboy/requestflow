using RequestFlow.Shared.Constants;

namespace RequestFlow.Domain.Entities;

public class Request
{
    public int Id { get; set; }
    public string RequestNo { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int RequestTypeId { get; set; }
    public Priority Priority { get; set; }
    public RequestStatus Status { get; set; }
    public string CreatedByUserId { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public RequestType RequestType { get; set; } = null!;
    public ICollection<RequestStatusHistory> StatusHistory { get; set; } = new List<RequestStatusHistory>();
}
