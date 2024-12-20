using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practic_API.Models;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        public PracticContext Context { get; }

        public FavoritesController(PracticContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetResult()
        {
            List<Favorite> favorites = Context.Favorites.ToList();
            return Ok(favorites);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Favorite? favorites = Context.Favorites.Where(x => x.Favoriteid == id).FirstOrDefault();
            if (favorites == null)
            {
                return BadRequest("not found");
            }
            return Ok(favorites);
        }
        [HttpPost]
        public IActionResult Add(int postid, int profileid, DateTime favorite_date)
        {
            Favorite favorites = new Favorite
            {
                Postid = postid,
                Profileid = profileid,
                FavoriteDate = favorite_date
            };

            Context.Favorites.Add(favorites);
            Context.SaveChanges();

            return Ok(favorites);
        }

        [HttpPut]
        public IActionResult Update(int favoriteid, int postid, int profileid, DateTime favorite_date)
        {
            Favorite? favorites = Context.Favorites.FirstOrDefault(x => x.Favoriteid == favoriteid);

            if (favorites == null)
            {
                return BadRequest("Profile not found");
            }
            favorites.FavoriteDate = favorite_date;
            Context.SaveChanges();
            return Ok(favorites);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Favorite? favorites = Context.Favorites.Where(x => x.Favoriteid == id).FirstOrDefault();
            if (favorites == null)
            {
                return BadRequest("not found");
            }
            Context.Favorites.Remove(favorites);
            Context.SaveChanges();
            return Ok();
        }
    }
}