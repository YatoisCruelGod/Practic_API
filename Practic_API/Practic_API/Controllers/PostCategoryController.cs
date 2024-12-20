using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practic_API.Models;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCategoryController : ControllerBase
    {
        public PracticContext Context { get; }

        public PostCategoryController(PracticContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetResult()
        {
            List<Postcategory> postcategories = Context.Postcategories.ToList();
            return Ok(postcategories);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Postcategory? postcategories = Context.Postcategories.Where(x => x.Categoryid == id).FirstOrDefault();
            if (postcategories == null)
            {
                return BadRequest("not found");
            }
            return Ok(postcategories);
        }
        [HttpPost]
        public IActionResult Add(string categoryname)
        {
            Postcategory? postcategories = new Postcategory
            {
                CategoryName = categoryname
            };

            Context.Postcategories.Add(postcategories);
            Context.SaveChanges();

            return Ok(postcategories);
        }

        [HttpPut]
        public IActionResult Update(int id, string categoryname)
        {
            Postcategory? postcategories = Context.Postcategories.FirstOrDefault(x => x.Categoryid == id);

            if (postcategories == null)
            {
                return BadRequest("Profile not found");
            }
            postcategories.CategoryName = categoryname;
            Context.SaveChanges();
            return Ok(postcategories);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Postcategory? postcategories = Context.Postcategories.Where(x => x.Categoryid == id).FirstOrDefault();
            if (postcategories == null)
            {
                return BadRequest("not found");
            }
            Context.Postcategories.Remove(postcategories);
            Context.SaveChanges();
            return Ok();
        }
    }
}