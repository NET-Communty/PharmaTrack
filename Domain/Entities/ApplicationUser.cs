
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public UserRole UserRole { get; set; }
        public bool IsDeleted { get; set; }
        public List<RefreshToken>? refreshTokens { get; set; }
    }
    public class Admin : ApplicationUser { }
    public enum UserRole
    {
        Admin = 1
    }
}
