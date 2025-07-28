using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services.Interface;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;

        public InvoiceService(IInvoiceRepository invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
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

            _invoiceRepo.Add(invoice);

            return invoice;
        }
    }
}
