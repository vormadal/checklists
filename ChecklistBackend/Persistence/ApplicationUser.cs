using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class ApplicationUser : IdentityUser, IApplicationUser
{
    public string FirstName { get; set; }
    public string FullName { get; set; }
    public string ImageUrl { get; set; }

}
