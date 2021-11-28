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
    public class CodeValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        
        [Required]
        public int CodeSetId { get; set; }
        [ForeignKey("CodeSetId")]

        [ValidateNever]
        public CodeSet CodeSet { get; set; }    
        
    }
}
