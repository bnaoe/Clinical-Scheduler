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
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderingDtTm { get; set; } = DateTime.Now;

        public string? OrderDetails { get; set; }

        public string Narrative { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        [ForeignKey("EncounterId")]
        public int EncounterId { get; set; }

        [ValidateNever]
        public Encounter Encounter { get; set; }

        public string? OrderingUserId { get; set; }
        [ForeignKey("ProviderUserId")]

        [ValidateNever]
        public ApplicationUser? OrderingUser { get; set; }

        [Required]
        [ForeignKey("OrderCatalogId")]
        public int OrderCatalogId { get; set; }

        [ValidateNever]
        public OrderCatalog OrderCatalog { get; set; }

        [Required]
        [ForeignKey("AdminRouteId")]
        [InverseProperty("AdminRouteCodeValues")]
        public int AdminRouteId { get; set; }

        [ValidateNever]
        public CodeValue AdminRoute { get; set; }

        [Required]
        [ForeignKey("AdminFreqId")]
        [InverseProperty("AdminFreqCodeValues")]
        public int AdminFreqId { get; set; }

        [ValidateNever]
        public CodeValue AdminFreq { get; set; }

        [Required]
        [ForeignKey("AdminTimeId")]
        [InverseProperty("AdminFreqCodeValues")]
        public int AdminTimeId { get; set; }

        [ValidateNever]
        public CodeValue AdminTime { get; set; }

        [Required]
        [ForeignKey("OrderStatusId")]
        [InverseProperty("OrderStatusCodeValues")]
        public int OrderStatusId { get; set; }

        [ValidateNever]
        public CodeValue OrderStatus { get; set; }


    }
}
