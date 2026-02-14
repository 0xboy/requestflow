using Microsoft.AspNetCore.Identity;

namespace RequestFlow.Persistence.Data;
public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
}
