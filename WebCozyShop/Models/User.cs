using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebCozyShop.Models;

[Index("Username", Name = "UQ__Users__536C85E4D6799292", IsUnique = true)]
[Index("Email", Name = "UQ__Users__A9D105347DB0543C", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Role { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? FullName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(11)]
    [Unicode(false)]
    public string? Phone { get; set; }

    public DateOnly? Dob { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("User")]
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
