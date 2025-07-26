using System;
using System.Collections.Generic;

namespace WebCozyShop.Models;

public partial class Token
{
    public string Email { get; set; } = null!;

    public string Token1 { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool? Status { get; set; }
}
