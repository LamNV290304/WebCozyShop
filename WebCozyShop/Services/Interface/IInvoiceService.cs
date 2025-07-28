using WebCozyShop.Models;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Services.Interface
{
    public interface IInvoiceService
    {
        Invoice CreateInvoice(List<TempInvoiceItem> items, int? userId);
    }
}
