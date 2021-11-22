using System.ComponentModel.DataAnnotations;

namespace Scheduler.Models
{
    public class CodeSet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
