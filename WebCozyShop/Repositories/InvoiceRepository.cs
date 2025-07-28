using Microsoft.EntityFrameworkCore;
using WebCozyShop.Helper;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public readonly CozyShopContext _CozyShopContext;

        public InvoiceRepository(CozyShopContext CozyShopContext)
        {
            _CozyShopContext = CozyShopContext;
        }

        public List<Invoice> GetAll()
        {
            return _CozyShopContext.Invoices.ToList();
        }

        public Invoice? GetById(int id)
        {
            return _CozyShopContext.Invoices
                .Include(i => i.InvoiceDetails)
                .ThenInclude(ii => ii.Variant)
                .FirstOrDefault(i => i.InvoiceId == id);
        }

        public void Add(Invoice invoice)
        {
            try
            {
                _CozyShopContext.Invoices.Add(invoice);
                _CozyShopContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the invoice.", ex);
            }
        }
    }
}
