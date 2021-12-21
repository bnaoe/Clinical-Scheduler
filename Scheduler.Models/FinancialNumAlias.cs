using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class FinancialNumAlias
    {
        private int _id;
        [Key]
        public int Id { get; set; }
        public string Fin { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }
    }
}
