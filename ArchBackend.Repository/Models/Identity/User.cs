using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ArchBackend.Core.Models.Identity
{
    public class User : IdentityUser<string>  // IdentityUser already provides 'Id' as string
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Role> UserRoles { get; set; }
    }
}
