using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practic_API.Models;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        public PracticContext Context { get; }

        public CommentsController(PracticContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetResult()
        {
            List<Comment> comments = Context.Comments.ToList();
            return Ok(comments);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Comment? comments = Context.Comments.Where(x => x.Commentid == id).FirstOrDefault();
            if (comments == null)
            {
                return BadRequest("not found");
            }
            return Ok(comments);
        }
        [HttpPost]
        public IActionResult Add(int postid, int observerprofileid, string textcomment, DateTime comment_date)
        {
            Comment? comments = new Comment
            {
                Postid = postid,
                Observerprofileid = observerprofileid,
                Textcomment = textcomment,
                CommentDate = comment_date
            };

            Context.Comments.Add(comments);
            Context.SaveChanges();

            return Ok(comments);
        }

        [HttpPut]
        public IActionResult Update(int commentid, int postid, int observerprofileid, string textcomment, DateTime comment_date)
        {
            Comment? comments = Context.Comments.FirstOrDefault(x => x.Commentid == postid);

            if (comments == null)
            {
                return BadRequest("not found");
            }
            comments.Commentid = commentid;
            comments.Postid = postid;
            comments.Observerprofileid = observerprofileid;
            comments.Textcomment = textcomment;
            comments.CommentDate = comment_date;
            Context.SaveChanges();
            return Ok(comments);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Comment? comments = Context.Comments.Where(x => x.Commentid == id).FirstOrDefault();
            if (comments == null)
            {
                return BadRequest("not found");
            }
            Context.Comments.Remove(comments);
            Context.SaveChanges();
            return Ok();
        }
    }
}