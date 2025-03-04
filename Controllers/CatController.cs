using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.DAL.Models;

namespace WebApplication1.Controllers
{
    [Controller]
    [Route("/api/cat")]
    public class CatController : ControllerBase
    {
        private AppDbContext context;

        public CatController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var models = context.Cats.ToList();

            return Ok(models);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cat model)
        {
            context.Cats.Add(model);
            context.SaveChanges();

            return Ok("Cat created");
        }
    }
}
