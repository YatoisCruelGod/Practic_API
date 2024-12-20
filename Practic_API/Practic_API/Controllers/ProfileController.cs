using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practic_API.Models;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        public PracticContext Context { get; }

        public ProfileController(PracticContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetResult()
        {
            List<Profile> profile = Context.Profiles.ToList();
            return Ok(profile);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Profile? profile = Context.Profiles.Where(x => x.Profileid == id).FirstOrDefault();
            if (profile == null)
            {
                return BadRequest("not found");
            }
            return Ok(profile);
        }
        [HttpPost]
        public IActionResult Add(string login, string password, string email, string firstName, string lastName, DateOnly dateOfBirth)
        {
            Profile? profile = new Profile
            {
                Login = login,
                Password = password,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
            };

            Context.Profiles.Add(profile);
            Context.SaveChanges();

            return Ok(profile);
        }

        [HttpPut]
        public IActionResult Update(int id, string login, string password, string email, string firstName, string lastName, DateOnly dateOfBirth)
        {
            Profile? profile = Context.Profiles.FirstOrDefault(x => x.Profileid == id);

            if (profile == null)
            {
                return BadRequest("Profile not found");
            }
            profile.Login = login;
            profile.Password = password;
            profile.Email = email;
            profile.FirstName = firstName;
            profile.LastName = lastName;
            profile.DateOfBirth = dateOfBirth;
            Context.SaveChanges();
            return Ok(profile);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Profile? profile = Context.Profiles.Where(x => x.Profileid == id).FirstOrDefault();
            if (profile == null)
            {
                return BadRequest("not found");
            }
            Context.Profiles.Remove(profile);
            Context.SaveChanges();
            return Ok();
        }
    }
}