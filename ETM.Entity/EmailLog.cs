using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Entity
{
    public class EmailLog
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public int EmailTemplateId { get; set; }
        [Required, MaxLength(50)]
        public string Sender { get; set; } = string.Empty;
        [Required]
        public string Receiver { get; set; } = string.Empty;
        public string Cc { get; set; } = string.Empty;
        public string Bcc { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string MessageBody { get; set; } = string.Empty;
        [Required]
        public byte SendStatus { get; set; } 

        public DateTime? DateSent { get; set; }

    }
}
