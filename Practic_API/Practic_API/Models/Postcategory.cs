using System;
using System.Collections.Generic;

namespace Practic_API.Models;

public partial class Postcategory
{
    public int Categoryid { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
