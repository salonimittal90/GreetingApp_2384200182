using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class UserDetailsEntity 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Hashed Password
        public string Salt { get; set; } // Salt for Hashing
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // ✅ Add these new properties
        public string? ResetToken { get; set; } // Password Reset Token
        public DateTime? ResetTokenExpiry { get; set; } // Token Expiry Time


    }
}
