using System;
using System.Collections.Generic;

namespace Practic_API.Models;

public partial class Comment
{
    public int Commentid { get; set; }

    public int Postid { get; set; }

    public int Observerprofileid { get; set; }

    public string Textcomment { get; set; } = null!;

    public DateTime? CommentDate { get; set; }

    public virtual Profile Observerprofile { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
