using Microsoft.AspNetCore.Mvc;

namespace WebCozyShop.Controllers
{
    public class ProductVariantController : Controller
    {

        [HttpGet]
        public IActionResult ProductVariantList(int productId)
        {
            
            return View();
        }
    }
}
