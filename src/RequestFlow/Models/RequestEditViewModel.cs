using System.ComponentModel.DataAnnotations;

namespace RequestFlow.Models;

public class RequestEditViewModel : RequestCreateViewModel
{
    public int Id { get; set; }
    public string Status { get; set; } = string.Empty;
}
