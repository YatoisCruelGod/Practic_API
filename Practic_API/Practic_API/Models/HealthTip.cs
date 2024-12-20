using System;
using System.Collections.Generic;

namespace Practic_API.Models;

public partial class HealthTip
{
    public int Tipid { get; set; }

    public int Creatorprofileid { get; set; }

    public string TipTitle { get; set; } = null!;

    public string TipText { get; set; } = null!;

    public DateTime? TipDate { get; set; }

    public virtual Profile Creatorprofile { get; set; } = null!;
}
