namespace RequestFlow.Models;

public class RequestDetailViewModel
{
    public int Id { get; set; }
    public string RequestNo { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string RequestType { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public bool CanEdit { get; set; }
    public bool CanApprove { get; set; }
}
