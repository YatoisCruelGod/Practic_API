using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practic_API.Models;
using System.Linq;
using System.Collections.Generic;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthTipsController : ControllerBase
    {
        public PracticContext Context { get; }

        public HealthTipsController(PracticContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<HealthTip> healthTips = Context.HealthTips.ToList();
            return Ok(healthTips);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            HealthTip? healthTip = Context.HealthTips.FirstOrDefault(ht => ht.Tipid == id);
            if (healthTip == null)
            {
                return NotFound("Health tip not found");
            }
            return Ok(healthTip);
        }

        [HttpPost]
        public IActionResult Add(int creatorProfileId, string tipTitle, string tipText)
        {
            HealthTip? healthTip = new HealthTip
            {
                Creatorprofileid = creatorProfileId,
                TipTitle = tipTitle,
                TipText = tipText,
                TipDate = DateTime.Now
            };

            Context.HealthTips.Add(healthTip);
            Context.SaveChanges();

            return Ok(healthTip);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, string tipTitle, string tipText)
        {
            HealthTip? healthTip = Context.HealthTips.FirstOrDefault(ht => ht.Tipid == id);

            if (healthTip == null)
            {
                return NotFound("Health tip not found");
            }

            healthTip.TipTitle = tipTitle;
            healthTip.TipText = tipText;
            Context.SaveChanges();

            return Ok(healthTip);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            HealthTip? healthTip = Context.HealthTips.FirstOrDefault(ht => ht.Tipid == id);
            if (healthTip == null)
            {
                return NotFound("Health tip not found");
            }

            Context.HealthTips.Remove(healthTip);
            Context.SaveChanges();

            return Ok();
        }
    }
}