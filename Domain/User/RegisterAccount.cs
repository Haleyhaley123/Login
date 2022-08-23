using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    [Table("RegisterAccounts")]
    public class RegisterAccount
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? VerifyCode { get; set; }
        public bool? Status { get; set; }
        public DateTime? DOB { get; set; }
        public string? Fullname { get; set; }
    }
}
