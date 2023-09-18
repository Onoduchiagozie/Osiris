using Microsoft.AspNetCore.Identity;

namespace Osiris.Models;

public class MyAppUser:IdentityUser
{
    public string Name { get; set; }
}