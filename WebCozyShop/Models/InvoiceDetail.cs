using System;
using System.Collections.Generic;

namespace WebCozyShop.Models;

public partial class InvoiceDetail
{
    public int InvoiceDetailId { get; set; }

    public int InvoiceId { get; set; }

    public int VariantId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual ProductVariant Variant { get; set; } = null!;
}
