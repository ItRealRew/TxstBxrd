using System;
using System.Collections.Generic;

namespace PERSONALITY_SERVICE.Models;

public partial class Detail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? LastName { get; set; }

    public virtual User User { get; set; } = null!;
}
