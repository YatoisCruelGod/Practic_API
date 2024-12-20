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

            /*foreach (var tagName in tags.Distinct())
            {
                Tag? tag = Context.Tags.FirstOrDefault(t => t.TagName == tagName);
                if (tag == null)
                {
                    tag = new Tag { TagName = tagName };
                    Context.Tags.Add(tag);
                    Context.SaveChanges();
                }

                var linkExists = Context.Database.ExecuteSqlRaw(
                    "SELECT COUNT(1) FROM post_tags WHERE postid = {0} AND tagid = {1}",
                    postid, tag.Tagid
                ) > 0;

                if (!linkExists)
                {
                    Context.Database.ExecuteSqlRaw(
                        "INSERT INTO post_tags (postid, tagid) VALUES ({0}, {1})",
                        postid, tag.Tagid
                    );
                }
            }*/

            return Ok("Tags added successfully");
        }


        [HttpPut]
        public IActionResult Update(int postid, int creatorprofileid, int categoryid, string post_title, string post_text, DateTime post_date, List<string> posttags)
        {
            Post? posts = Context.Posts.FirstOrDefault(x => x.Postid == postid);
            if (posts == null)
            {
                return BadRequest("Post not found");
            }
            posts.Creatorprofileid = creatorprofileid;
            posts.Categoryid = categoryid;
            posts.PostTitle = post_title;
            posts.PostText = post_text;
            posts.PostDate = post_date;
            Context.SaveChanges();
            Context.Database.ExecuteSqlRaw(
                "DELETE FROM post_tags WHERE postid = {0}",
                postid
            );

            foreach (var tagName in posttags)
            {
                Tag? tag = Context.Tags.FirstOrDefault(t => t.TagName == tagName);
                if (tag == null)
                {
                    tag = new Tag { TagName = tagName };
                    Context.Tags.Add(tag);
                    Context.SaveChanges();
                }
                Context.Database.ExecuteSqlRaw(
                    "INSERT INTO post_tags (postid, tagid) VALUES ({0}, {1})",
                    postid, tag.Tagid
                );
            }
            return Ok(posts);
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