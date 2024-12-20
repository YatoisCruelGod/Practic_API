using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practic_API.Models;
using System.Collections.Generic;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        public PracticContext Context { get; }

        public RecipeIngredientController(PracticContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetResult()
        {
            List<Recipeingredient> recipeingredients = Context.Recipeingredients.ToList();
            return Ok(recipeingredients);
        }
        [HttpGet("{id}")]
        public IActionResult GetByIds(int recipeid, string ingredient_name)
        {
            Recipeingredient? recipeingredients = Context.Recipeingredients
                .Where(x => x.Recipeid == recipeid && x.IngredientName == ingredient_name)
                .FirstOrDefault();

            if (recipeingredients == null)
            {
                return NotFound("Subscription not found");
            }

            return Ok(recipeingredients);
        }
        [HttpPost]
        public IActionResult Add(int recipeid, string ingredient_name, string amount)
        {
            Recipeingredient? existingIngredient = Context.Recipeingredients
                .FirstOrDefault(x => x.Recipeid == recipeid && x.IngredientName == ingredient_name);

            if (existingIngredient != null)
            {
                return Conflict("This ingredient already exists for the given recipe.");
            }
            Recipeingredient newIngredient = new Recipeingredient
            {
                Recipeid = recipeid,
                IngredientName = ingredient_name,
                Amount = amount
            };
            Context.Recipeingredients.Add(newIngredient);
            Context.SaveChanges();
            return Ok(newIngredient);
        }



        [HttpPut]
        public IActionResult Update(int recipeid, string ingredient_name, string new_amount)
        {
            Recipeingredient? ingredient = Context.Recipeingredients
                .FirstOrDefault(x => x.Recipeid == recipeid && x.IngredientName == ingredient_name);

            if (ingredient == null)
            {
                return NotFound("The ingredient for the specified recipe was not found.");
            }
            ingredient.IngredientName = ingredient_name;
            ingredient.Amount = new_amount;
            Context.SaveChanges();
            return Ok(ingredient);
        }



        [HttpDelete]
        public IActionResult Delete(int recipeid, string ingredient_name)
        {
            Recipeingredient? ingredient = Context.Recipeingredients
                .FirstOrDefault(x => x.Recipeid == recipeid && x.IngredientName == ingredient_name);
            if (ingredient == null)
            {
                return BadRequest("not found");
            }
            Context.Recipeingredients.Remove(ingredient);
            Context.SaveChanges();
            return Ok();
        }
    }
}