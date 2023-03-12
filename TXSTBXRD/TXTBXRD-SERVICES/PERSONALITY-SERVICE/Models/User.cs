using System;
using System.Collections.Generic;

namespace PERSONALITY_SERVICE.Models;

/// <summary>
/// Table &quot;Users&quot;
/// </summary>
public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Enabled { get; set; }

    public virtual ICollection<Detail> Details { get; } = new List<Detail>();

    public virtual ICollection<Salt> Salts { get; } = new List<Salt>();

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();
}
