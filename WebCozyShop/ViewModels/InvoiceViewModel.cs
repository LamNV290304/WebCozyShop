namespace WebCozyShop.ViewModels
{
    public class TempInvoiceItem
    {
        public int VariantId { get; set; }
        public string Name { get; set; } = "";
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Total => UnitPrice * Quantity;
    }

    public class InvoiceViewModel
    {
        public Guid TempUserId { get; set; } = Guid.NewGuid();
        public List<TempInvoiceItem> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.Total);
    }
}
