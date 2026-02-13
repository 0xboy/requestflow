using System.ComponentModel.DataAnnotations;

namespace RequestFlow.Models;

public class RequestCreateViewModel
{
    [Required]
    [StringLength(200)]
    [Display(Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Request Type")]
    public int RequestTypeId { get; set; }

    [Required]
    [Display(Name = "Priority")]
    public string Priority { get; set; } = "Medium"; // Low, Medium, High
}
