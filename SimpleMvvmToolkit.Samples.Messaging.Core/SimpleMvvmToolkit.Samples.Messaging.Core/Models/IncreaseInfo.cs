namespace SimpleMvvmSamples.Messaging.Core.Models
{
    public class IncreaseInfo
    {
        public string CustomerName { get; set; }
        public int Amount { get; set; }
        public IncreaseInfo(string customerName, int amount)
        {
            CustomerName = customerName;
            Amount = amount;
        }
    }
}
