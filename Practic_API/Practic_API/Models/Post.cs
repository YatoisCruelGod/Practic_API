using System;
using System.Collections.Generic;

namespace Practic_API.Models;

public partial class Post
{
    public int Postid { get; set; }

    public int Creatorprofileid { get; set; }

    public int Categoryid { get; set; }

    public string PostTitle { get; set; } = null!;

    public string PostText { get; set; } = null!;

    public DateTime? PostDate { get; set; }

    public virtual Postcategory Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Profile Creatorprofile { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Recipeingredient> Recipeingredients { get; set; } = new List<Recipeingredient>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
