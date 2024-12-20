using System;
using System.Collections.Generic;

namespace Practic_API.Models;

public partial class Tag
{
    public int Tagid { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
