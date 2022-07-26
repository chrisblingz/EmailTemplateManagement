using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Entity
{
    public class EmailTemplate
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string Template { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Subject { get; set; } = string.Empty;
        [Required, MaxLength(10)]
        public string Code { get; set; } = string.Empty;
    }
}
