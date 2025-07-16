using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Saas.Models
{
    public class Payment{
        public int PaymentId { get; set; }
        [JsonIgnore]
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string PaymentStatus { get; set; }
        [Required]
        public int PaymentStatusId { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId  { get; set; }
        public virtual Tenant Tenant { get; set; }

        [Required]
        [ForeignKey("Plan")]
        public int PlanId { get; set; } 
        public virtual Plan Plan { get; set; }

        public string GatewayTransactionId { get; set; }

        public string InvoiceId { get; set; }
        public DateTime? InvoiceIssueDate { get; set; }
        public string InvoiceUrl { get; set; }
    }
}
