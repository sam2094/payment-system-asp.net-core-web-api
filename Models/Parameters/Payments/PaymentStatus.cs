namespace Models.Parameters.Payments
{
    public class PaymentStatusInput
    {
        public long PaymentId { get; set; }
        public string ExternalTransactionId { get; set; }
    }
}
