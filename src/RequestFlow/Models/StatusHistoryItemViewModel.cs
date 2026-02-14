namespace RequestFlow.Models;

public class StatusHistoryItemViewModel
{
    public string FromStatus { get; set; } = string.Empty;
    public string ToStatus { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public string ChangedByUserId { get; set; } = string.Empty;
    public string ChangedByDisplayName { get; set; } = string.Empty;
    public DateTime ChangedDate { get; set; }
}
