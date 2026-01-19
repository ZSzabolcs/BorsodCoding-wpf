using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    public class Mezo
    {
        public Mezo() { }

        public string Id { get; set; }
    }

    public class SaveMezoi : Mezo
    {
        public SaveMezoi() { }
        public int Points { get; set; }
        public int Level { get; set; }
        public string Language { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime ModDate { get; set; }
    }

    public class UserMezoi : Mezo
    {
        public UserMezoi() { }

        public DateTime? BirthDate { get; set; }

        public string? UserName { get; set; }

        public string? NormalizedUserName { get; set; }

        public string? Email { get; set; }

        public string? NormalizedEmail { get; set; }

        public bool? EmailConfirmed { get; set; }

        public string? PasswordHash { get; set; }

        public string? SecurityStamp { get; set; }

        public string? ConcurrencyStamp { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? PhoneNumberConfirmed { get; set; }

        public bool? TwoFactorEnabled { get; set; }

        public DateTime? LockoutEnd { get; set; }

        public bool? LockoutEnabled { get; set; }

        public int? AccessFailedCount { get; set; }

        public DateTime? ModDate { get; set; }
    }
}
