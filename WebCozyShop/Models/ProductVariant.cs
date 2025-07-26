using System;
using System.Collections.Generic;

namespace WebCozyShop.Models;

public partial class ProductVariant
{
    public int VariantId { get; set; }

    public int ProductId { get; set; }

    public string Sku { get; set; } = null!;

    public string? Color { get; set; }

    public string? Size { get; set; }

    public decimal Price { get; set; }

    public int? StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Product Product { get; set; } = null!;
}
