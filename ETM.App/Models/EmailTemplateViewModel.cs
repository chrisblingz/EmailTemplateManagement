using System.ComponentModel.DataAnnotations;

namespace ETM.App.Models
{
    public class EmailTemplateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Template { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
