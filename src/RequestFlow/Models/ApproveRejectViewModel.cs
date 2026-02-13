using System.ComponentModel.DataAnnotations;

namespace RequestFlow.Models;

public class ApproveRejectViewModel
{
    public int RequestId { get; set; }
    public string RequestNo { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Rejection reason (required when rejecting)")]
    public string? RejectionReason { get; set; }
}
