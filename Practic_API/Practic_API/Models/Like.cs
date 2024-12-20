using System;
using System.Collections.Generic;

namespace Practic_API.Models;

public partial class Like
{
    public int Likeid { get; set; }

    public int Postid { get; set; }

    public int Profileid { get; set; }

    public DateTime? LikeDate { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;
}
