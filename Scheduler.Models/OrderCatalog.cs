using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class OrderCatalog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public int CodeValueId { get; set; }
        [ForeignKey("CodeValueId")]

        [ValidateNever]
        public CodeValue CodeValue { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

       
    }
}
