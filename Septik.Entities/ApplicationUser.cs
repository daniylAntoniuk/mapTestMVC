using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Septik.Entities
{
    public partial class ApplicationUser : IdentityUser<long>
    {

    }

    public partial class UserRole : IdentityRole<long>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual Role Role { get; set; }
    }

    public partial class Role : IdentityRole<long>
    {
        public Role()
        {

        }
        public Role(string role)
        {
            Name = role;
        }
    }
}
