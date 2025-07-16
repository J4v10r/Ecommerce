using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Saas.Models
{
    public class OrderPayment{
        [Key]
        public int OrderPaymentId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; } 
        public virtual Tenant Tenant { get; set; }

        public string GatewayTransactionId { get; set; }

        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceIssueDate { get; set; }
        public string InvoiceUrl { get; set; }


    }
}
