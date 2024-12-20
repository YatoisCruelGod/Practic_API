using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practic_API.Models;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        public PracticContext Context { get; }

        public TagsController(PracticContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetResult()
        {
            List<Tag> tags = Context.Tags.ToList();
            return Ok(tags);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Tag? tags = Context.Tags.Where(x => x.Tagid == id).FirstOrDefault();
            if (tags == null)
            {
                return BadRequest("not found");
            }
            return Ok(tags);
        }
        [HttpPost]
        public IActionResult Add(int tagid, string tag_name)
        {
            Tag? tag = new Tag
            {
                Tagid = tagid
            };

            Context.Tags.Add(tag);
            Context.SaveChanges();

            return Ok(tag);
        }

        [HttpPut]
        public IActionResult Update(int tagid, string tag_name)
        {
            Tag? tag = Context.Tags.FirstOrDefault(x => x.Tagid == tagid);

            if (tag == null)
            {
                return BadRequest("Profile not found");
            }
            tag.TagName = tag_name;
            Context.SaveChanges();
            return Ok(tag);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Tag? tag = Context.Tags.Where(x => x.Tagid == id).FirstOrDefault();
            if (tag == null)
            {
                return BadRequest("not found");
            }
            Context.Tags.Remove(tag);
            Context.SaveChanges();
            return Ok();
        }
    }
}