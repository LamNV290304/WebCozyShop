using Microsoft.AspNetCore.Mvc;
using WebCozyShop.Helper;
using WebCozyShop.Services.Interface;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IProductVariantService _productVariantService;

        private const string SessionInvoices = "TempInvoices";
        private const string SessionActiveId = "TempUserId";

        public InvoiceController(IInvoiceService invoiceService, IProductVariantService productVariantService)
        {
            _invoiceService = invoiceService;
            _productVariantService = productVariantService;
        }

        private Dictionary<Guid, List<TempInvoiceItem>> GetAllInvoices()
        {
            return HttpContext.Session.GetObject<Dictionary<Guid, List<TempInvoiceItem>>>(SessionInvoices) ?? new();
        }

        private void SaveAllInvoices(Dictionary<Guid, List<TempInvoiceItem>> data)
        {
            HttpContext.Session.SetObject(SessionInvoices, data);
        }

        private Guid GetOrCreateActiveUserId(Dictionary<Guid, List<TempInvoiceItem>> invoices)
        {
            var idStr = HttpContext.Session.GetString(SessionActiveId);
            if (!Guid.TryParse(idStr, out Guid id))
            {
                id = Guid.NewGuid();
                HttpContext.Session.SetString(SessionActiveId, id.ToString());
                invoices[id] = new();
                SaveAllInvoices(invoices);
            }
            return id;
        }

        [HttpGet]
        public IActionResult SwitchInvoice(Guid tempUserId)
        {
            HttpContext.Session.SetString(SessionActiveId, tempUserId.ToString());
            return RedirectToAction("CreateInvoice");
        }

        [HttpPost]
        public IActionResult NewInvoice()
        {
            var invoices = GetAllInvoices();
            var newId = Guid.NewGuid();
            invoices[newId] = new();
            SaveAllInvoices(invoices);

            HttpContext.Session.SetString(SessionActiveId, newId.ToString());
            return RedirectToAction("CreateInvoice");
        }

        [HttpPost]
        public IActionResult DeleteInvoice(Guid tempUserId)
        {
            var invoices = GetAllInvoices();
            if (invoices.ContainsKey(tempUserId))
                invoices.Remove(tempUserId);

            SaveAllInvoices(invoices);

            var current = HttpContext.Session.GetString(SessionActiveId);
            if (current == tempUserId.ToString())
            {
                HttpContext.Session.Remove(SessionActiveId);
            }

            return RedirectToAction("CreateInvoice");
        }

        [HttpGet]
        public IActionResult CreateInvoice()
        {
            var invoices = GetAllInvoices();
            var currentId = GetOrCreateActiveUserId(invoices);
            invoices.TryGetValue(currentId, out var currentItems);

            var others = invoices
                .Where(i => i.Key != currentId && i.Value.Any())
                .Select(i => (i.Key, i.Value.Count, i.Value.Sum(p => p.Total)))
                .ToList();

            var vm = new CreateInvoiceViewModel
            {
                TempUserId = currentId,
                CartItems = currentItems ?? new(),
                OtherOpenInvoices = others
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddBySku(Guid tempUserId, string sku)
        {
            var invoices = GetAllInvoices();
            if (!invoices.ContainsKey(tempUserId))
                invoices[tempUserId] = new();

            var items = invoices[tempUserId];
            var variant = _productVariantService.GetProductVariantBySku(sku);
            if (variant == null)
            {
                TempData["Error"] = "SKU not exist.";
                return RedirectToAction("CreateInvoice");
            }

            var existing = items.FirstOrDefault(i => i.VariantId == variant.VariantId);
            if (existing != null)
            {
                if (existing.Quantity + 1 > variant.StockQuantity)
                {
                    TempData["Error"] = $"Only {variant.StockQuantity} product left.";
                    return RedirectToAction("CreateInvoice");
                }
                existing.Quantity++;
            }
            else
            {
                if (variant.StockQuantity < 1)
                {
                    TempData["Error"] = "Sold out.";
                    return RedirectToAction("CreateInvoice");
                }

                items.Add(new TempInvoiceItem
                {
                    VariantId = variant.VariantId,
                    Name = variant.Sku,
                    Color = variant.Color,
                    Size = variant.Size,
                    Quantity = 1,
                    UnitPrice = variant.Price,
                    ImageUrl = variant.ImageUrl
                });
            }

            SaveAllInvoices(invoices);
            return RedirectToAction("CreateInvoice");
        }

        [HttpPost]
        public IActionResult RemoveItem(Guid tempUserId, int variantId)
        {
            var invoices = GetAllInvoices();
            if (invoices.ContainsKey(tempUserId))
            {
                invoices[tempUserId].RemoveAll(i => i.VariantId == variantId);
                SaveAllInvoices(invoices);
            }

            return RedirectToAction("CreateInvoice");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(Guid tempUserId, int variantId, int quantity)
        {
            if (quantity < 1)
            {
                TempData["Error"] = "Must > 0.";
                return RedirectToAction("CreateInvoice");
            }

            var invoices = GetAllInvoices();
            if (!invoices.TryGetValue(tempUserId, out var items))
            {
                TempData["Error"] = "Invoice not found.";
                return RedirectToAction("CreateInvoice");
            }

            var item = items.FirstOrDefault(i => i.VariantId == variantId);
            if (item == null)
            {
                TempData["Error"] = "Do not found product.";
                return RedirectToAction("CreateInvoice");
            }

            var variant = _productVariantService.GetProductVariantById(variantId);
            if (variant == null || quantity > variant.StockQuantity)
            {
                TempData["Error"] = $" {variant?.StockQuantity ?? 0} left.";
                return RedirectToAction("CreateInvoice");
            }

            item.Quantity = quantity;
            SaveAllInvoices(invoices);

            return RedirectToAction("CreateInvoice");
        }

        [HttpPost]
        public IActionResult Checkout(Guid tempUserId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            var invoices = GetAllInvoices();
            if (!invoices.TryGetValue(tempUserId, out var items) || !items.Any())
            {
                TempData["Error"] = "Nothing to check out.";
                return RedirectToAction("CreateInvoice");
            }

            var invoice = _invoiceService.CreateInvoice(items, userId);
            invoices.Remove(tempUserId);
            SaveAllInvoices(invoices);

            TempData["Success"] = $"Check out invoice #{invoice.InvoiceId}";
            return RedirectToAction("CreateInvoice");
        }
    }
}
