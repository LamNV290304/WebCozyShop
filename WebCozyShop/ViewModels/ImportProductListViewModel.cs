namespace WebCozyShop.ViewModels
{
    public class ImportProductListViewModel
    {
        public List<ImportItemViewModel> Items { get; set; } = new();
    }

    public class ImportItemViewModel
    {
        public string Sku { get; set; } = "";
        public int QuantityToImport { get; set; }
        public string? Error { get; set; }
    }
}
