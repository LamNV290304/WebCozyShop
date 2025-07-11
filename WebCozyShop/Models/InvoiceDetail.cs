using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebCozyShop.Models;

public partial class InvoiceDetail
{
    [Key]
    public int InvoiceDetailID { get; set; }

    public int InvoiceID { get; set; }

    public int VariantID { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal UnitPrice { get; set; }

    [ForeignKey("InvoiceID")]
    [InverseProperty("InvoiceDetails")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("VariantID")]
    [InverseProperty("InvoiceDetails")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
