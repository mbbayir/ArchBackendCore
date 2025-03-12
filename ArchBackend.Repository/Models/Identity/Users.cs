    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    namespace ArchBackend.Core.Models.Identity
    {
        public class Users : IdentityUser<int>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

        }
    }
