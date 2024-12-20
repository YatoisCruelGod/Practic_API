using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practic_API.Models;
using System.Collections.Generic;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        public PracticContext Context { get; }

        public FollowersController(PracticContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetResult()
        {
            List<Follower> followers = Context.Followers.ToList();
            return Ok(followers);
        }
        [HttpGet("{id}")]
        public IActionResult GetByIds(int followerid, int followedid)
        {
            Follower? follower = Context.Followers
                .Where(x => x.Followerid == followerid && x.Followedid == followedid)
                .FirstOrDefault();

            if (follower == null)
            {
                return NotFound("Subscription not found");
            }

            return Ok(follower);
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Add(int followerid, int followedid, DateTime follow_date)
        {
            Follower? existingFollower = Context.Followers
                .FirstOrDefault(x => x.Followerid == followerid && x.Followedid == followedid);

            if (existingFollower != null)
            {
                return Conflict("This subscription already exists.");
            }

            Follower follower = new Follower
            {
                Followerid = followerid,
                Followedid = followedid,
                FollowDate = follow_date
            };
            Context.Followers.Add(follower);
            Context.SaveChanges();
            return Ok(follower);
        }


        [HttpPut]
        public IActionResult Update(int followerid, int followedid, DateTime follow_date)
        {
            Follower? follower = Context.Followers
                .FirstOrDefault(x => x.Followerid == followerid && x.Followedid == followedid);

            if (follower == null)
            {
                return NotFound("Subscription not found");
            }
            follower.FollowDate = follow_date;
            Context.SaveChanges();
            return Ok(follower);
        }


        [HttpDelete]
        public IActionResult Delete(int followerid, int followedid)
        {
            Follower? follower = Context.Followers
                .FirstOrDefault(x => x.Followerid == followerid && x.Followedid == followedid);
            if (follower == null)
            {
                return BadRequest("not found");
            }
            Context.Followers.Remove(follower);
            Context.SaveChanges();
            return Ok();
        }
    }
}