using System;
using System.Collections.Generic;

namespace Practic_API.Models;

public partial class Recipeingredient
{
    public int Recipeid { get; set; }

    public string IngredientName { get; set; } = null!;

    public string Amount { get; set; } = null!;

    public virtual Post Recipe { get; set; } = null!;
}
