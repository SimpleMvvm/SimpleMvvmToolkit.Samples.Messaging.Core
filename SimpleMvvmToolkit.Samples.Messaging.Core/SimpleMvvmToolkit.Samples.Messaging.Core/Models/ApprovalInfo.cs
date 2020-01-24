namespace SimpleMvvmSamples.Messaging.Core.Models
{
    public class ApprovalInfo
    {
        public string CustomerName { get; set; }
        public int Amount { get; set; }
        public bool Result { get; set; }
        public ApprovalInfo(string customerName, int amount, bool result)
        {
            CustomerName = customerName;
            Amount = amount;
            Result = result;
        }
    }
}
