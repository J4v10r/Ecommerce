namespace Saas_Sexshop.Dto.PaymentDto
{
    public class PaymentCreatDto{
        public decimal PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public int PaymentStatusId { get; set; }

        public int TenantId { get; set; }
        public int PlanId { get; set; }

        public string GatewayTransactionId { get; set; }
        public string InvoiceId { get; set; }
        public DateTime? InvoiceIssueDate { get; set; }
        public string InvoiceUrl { get; set; }
    }
}
