using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebCozyShop.Models;

[Index("SKU", Name = "UQ__ProductV__CA1ECF0D9E9E2EFE", IsUnique = true)]
public partial class ProductVariant
{
    [Key]
    public int VariantID { get; set; }

    public int ProductID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SKU { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Color { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Size { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public int? StockQuantity { get; set; }

    public bool? LowStockAlert { get; set; }

    [InverseProperty("Variant")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [ForeignKey("ProductID")]
    [InverseProperty("ProductVariants")]
    public virtual Product Product { get; set; } = null!;

    [InverseProperty("Variant")]
    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();
}
