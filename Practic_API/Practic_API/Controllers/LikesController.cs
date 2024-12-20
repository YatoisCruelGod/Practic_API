using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practic_API.Models;
using System.Collections.Generic;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        public PracticContext Context { get; }

        public LikesController(PracticContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetResult()
        {
            List<Like> likes = Context.Likes.ToList();
            return Ok(likes);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Like? likes = Context.Likes.Where(x => x.Likeid == id).FirstOrDefault();
            if (likes == null)
            {
                return BadRequest("not found");
            }
            return Ok(likes);
        }
        [HttpPost]
        public IActionResult Add(int postid, int profileid, DateTime like_date)
        {
            Like? likes = new Like
            {
                Postid = postid,
                Profileid = profileid,
                LikeDate = like_date
            };

            Context.Likes.Add(likes);
            Context.SaveChanges();

            return Ok(likes);
        }

        [HttpPut]
        public IActionResult Update(int likeid, int postid, int profileid, DateTime like_date)
        {
            Like? likes = Context.Likes.FirstOrDefault(x => x.Likeid == postid);

            if (likes == null)
            {
                return BadRequest("Profile not found");
            }
            likes.Postid = postid;
            likes.Profileid = profileid;
            likes.LikeDate = like_date;
            Context.SaveChanges();
            return Ok(likes);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Like? likes = Context.Likes.Where(x => x.Likeid == id).FirstOrDefault();
            if (likes == null)
            {
                return BadRequest("not found");
            }
            Context.Likes.Remove(likes);
            Context.SaveChanges();
            return Ok();
        }
    }
}