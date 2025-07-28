using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services.Interface;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IProductVariantRepository _variantRepo;

        public InvoiceService(IInvoiceRepository invoiceRepo, IProductVariantRepository productVariantRepository)
        {
            _invoiceRepo = invoiceRepo;
            _variantRepo = productVariantRepository;
        }

        public Invoice CreateInvoice(List<TempInvoiceItem> items, int? userId)
        {
            var invoice = new Invoice
            {
                UserId = userId ?? 0,
                CreatedAt = DateTime.Now,
                TotalAmount = items.Sum(x => x.UnitPrice * x.Quantity),
                InvoiceDetails = items.Select(x => new InvoiceDetail
                {
                    VariantId = x.VariantId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };

            
            foreach (var item in items)
            {
                var variant = _variantRepo.GetProductVariantById(item.VariantId);
                if (variant != null)
                {
                    if (variant.StockQuantity < item.Quantity)
                    {
                        throw new Exception($"❌ Not enought SKU: {variant.Sku} (left {variant.StockQuantity}, need {item.Quantity})");
                    }

                    var newQuantity = variant.StockQuantity - item.Quantity;
                    var success = _variantRepo.UpdateStockQuantity(variant.VariantId, newQuantity);
                    if (!success)
                    {
                        throw new Exception($"❌ Failed update SKU: {variant.Sku}");
                    }
                }
                else
                {
                    throw new Exception($"❌ Not found item id: {item.VariantId}");
                }
            }

            _invoiceRepo.Add(invoice);

            return invoice;
        }
    }
}
