using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Saas.Models;

public class Order
{
    [Key]
    [JsonIgnore]
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public string Status { get; set; } = "Pendente";

    [ForeignKey("OrderPayment")]
    public int? OrderPaymentId { get; set; }
    public virtual OrderPayment? OrderPayment { get; set; }

    [ForeignKey("Tenant")]
    public int TenantId { get; set; }
    public virtual Tenant Tenant { get; set; }
}