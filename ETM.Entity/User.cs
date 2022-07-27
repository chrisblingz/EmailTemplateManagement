using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Username { get; set; } = string.Empty;
        [Required, MaxLength(25)]
        public string Firstname { get; set; } = string.Empty;
        [MaxLength(25)]
        public string Middlename { get; set; } = string.Empty;
        [Required, MaxLength(25)]
        public string Lastname { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Passcode { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        public int Channel { get; set; }
    }
}
