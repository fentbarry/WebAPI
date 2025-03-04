using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;

namespace WebApplication1.Controllers
{
    [Controller]
    [Route("api/role")]
    public class RoleContoller : ControllerBase
    {
        private AppDbContext context;
        private RoleManager<IdentityRole> roleManager;

        public RoleContoller(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.roleManager = roleManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] IdentityRole role)
        {
            if (await roleManager.RoleExistsAsync(role.Id))
                return BadRequest("Role already exists");

            await roleManager.CreateAsync(role);
            return Ok();
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete([FromHeader] string id)
        {
            if (!await roleManager.RoleExistsAsync(id))
                return NotFound();

            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound();

            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetById(string id)
        {
            var roles = await roleManager.Roles.FirstOrDefaultAsync(p => p.Id == id);

            if (roles == null)
                return NotFound();

            return Ok(roles);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await roleManager.Roles.ToListAsync());
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] IdentityRole updatedRole)
        {
            var role = await roleManager.FindByIdAsync(updatedRole.Id);

            if (role == null)
                return NotFound();

            role.Name = updatedRole.Name;

            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return Ok("Role updated");

            return BadRequest(result.Errors);
        }
    }
}