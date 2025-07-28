using WebCozyShop.Models;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Repositories.Interface
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetAll();
        Invoice? GetById(int id);
        void Add(Invoice invoice);
    }
}
