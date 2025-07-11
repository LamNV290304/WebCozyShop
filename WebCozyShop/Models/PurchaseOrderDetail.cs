using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebCozyShop.Models;

public partial class PurchaseOrderDetail
{
    [Key]
    public int PODetailID { get; set; }

    public int POID { get; set; }

    public int VariantID { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? CostPrice { get; set; }

    [ForeignKey("POID")]
    [InverseProperty("PurchaseOrderDetails")]
    public virtual PurchaseOrder PO { get; set; } = null!;

    [ForeignKey("VariantID")]
    [InverseProperty("PurchaseOrderDetails")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
