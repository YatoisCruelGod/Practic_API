using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practic_API.Models;
using Practic_API.Payloads;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public PracticContext Context { get; }

        public PostController(PracticContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Post> posts = Context.Posts
                .Include(p => p.Tags)
                .Include(x => x.Comments)
                .Include(x => x.Likes)
                .Include(x => x.Favorites)
                .ToList();

            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found");
            }

            var postResponses = posts.Select(post => new PostResponse(post)).ToList();

            return Ok(postResponses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Post? posts = Context.Posts.Where(x => x.Postid == id)
                .Include(x => x.Tags)
                .Include(x => x.Comments)
                .Include(x => x.Likes)
                .Include(x => x.Favorites)
                .FirstOrDefault();

            if (posts == null)
            {
                return BadRequest("not found");
            }

            return Ok(new PostResponse(posts));
        }

        [HttpPost("AddPost")]
        public IActionResult AddPost(int creatorprofileid, int categoryid, string post_title, string post_text, DateTime post_date)
        {
            Post? posts = new Post
            {
                Creatorprofileid = creatorprofileid,
                Categoryid = categoryid,
                PostTitle = post_title,
                PostText = post_text,
                PostDate = post_date
            };
            Context.Posts.Add(posts);
            Context.SaveChanges();
            return Ok(posts);
        }
        [HttpPost("AddTags")]
        public IActionResult AddTagsToPost(int postid, List<string> tags)
        {
            Post? post = Context.Posts.FirstOrDefault(p => p.Postid == postid);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            List<Tag> tagEntities = tags.Select(tagName => new Tag { TagName = tagName })
                .ToList();

            Context.AddRange(tagEntities);
            Context.SaveChanges();

            post.Tags = tagEntities;

            Context.SaveChanges();

            return Ok("Tags added successfully");
        }


        [HttpPut]
        public IActionResult Update(int postid, int creatorprofileid, int categoryid, string post_title, string post_text, DateTime post_date, List<string> posttags)
        {
            Post? post = Context.Posts
                .Include(p => p.Tags)
                .FirstOrDefault(x => x.Postid == postid);

            if (post == null)
            {
                return BadRequest("Post not found");
            }

            post.Creatorprofileid = creatorprofileid;
            post.Categoryid = categoryid;
            post.PostTitle = post_title;
            post.PostText = post_text;
            post.PostDate = post_date;

            post.Tags.Clear();

            foreach (var tagName in posttags)
            {
                Tag? tag = Context.Tags.FirstOrDefault(t => t.TagName == tagName);
                if (tag == null)
                {
                    tag = new Tag { TagName = tagName };
                    Context.Tags.Add(tag);
                }
                post.Tags.Add(tag);
            }

            Context.SaveChanges();

            return Ok(post);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Post? posts = Context.Posts.Where(x => x.Postid == id).FirstOrDefault();
            if (posts == null)
            {
                return BadRequest("not found");
            }
            Context.Posts.Remove(posts);
            Context.SaveChanges();
            return Ok();
        }
    }
}