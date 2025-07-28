namespace WebCozyShop.ViewModels
{
    public class CreateInvoiceViewModel
    {
        public Guid TempUserId { get; set; }
        public List<TempInvoiceItem> CartItems { get; set; } = new();
        public decimal TotalAmount => CartItems.Sum(i => i.Total);
        public List<(Guid TempUserId, int Count, decimal Total)> OtherOpenInvoices { get; set; } = new();
    }
}
