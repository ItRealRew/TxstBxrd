using System;
using System.Collections.Generic;

namespace PERSONALITY_SERVICE.Models;

public partial class Salt
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Value { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
