namespace RequestFlow.Models;

public class RequestListViewModel
{
    public int Id { get; set; }
    public string RequestNo { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
