using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebCozyShop.Models;

public partial class PurchaseOrder
{
    [Key]
    public int POID { get; set; }

    public int UserID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "text")]
    public string? Note { get; set; }

    [InverseProperty("PO")]
    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    [ForeignKey("UserID")]
    [InverseProperty("PurchaseOrders")]
    public virtual User User { get; set; } = null!;
}
