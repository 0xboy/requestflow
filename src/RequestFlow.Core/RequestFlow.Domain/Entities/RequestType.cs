using RequestFlow.Shared.Constants;

namespace RequestFlow.Domain.Entities;

public class RequestType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Request> Requests { get; set; } = new List<Request>();
}
