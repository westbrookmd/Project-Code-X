namespace ProjectCodeX.Models
{
    public class FundViewModel
    {
        public List<Purchase> Purchases { get; set; } = new();
        public Purchase PurchaseDetail { get; set; } = new();
    }
}
