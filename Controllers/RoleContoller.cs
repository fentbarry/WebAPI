using Microsoft.AspNetCore.Mvc;
using WebApplication1.BLL.DTOs.Role;
using WebApplication1.BLL.Services.Role;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleContoller : ControllerBase
    {
        private IRoleService roleService;

        public RoleContoller(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await roleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id required");

            var role = await roleService.GetByIdAsync(id);

            return role == null ? BadRequest("Role not found") : Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RoleDTO dto)
        {
            var result = await roleService.CreateAsync(dto);

            return result ? Ok("Role created") : BadRequest("Role not created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] RoleDTO dto)
        {
            var result = await roleService.UpdateAsync(dto);

            return result ? Ok("Role updated") : BadRequest("Role not updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id required");

            var result = await roleService.DeleteAsync(id);
            return result ? Ok("Role deleted") : BadRequest("Role not deleted");
        }
    }
}