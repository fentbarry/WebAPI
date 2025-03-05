using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.BLL.DTOs.Role;
using WebApplication1.BLL.Services.Role;
using WebApplication1.DAL;

namespace WebApplication1.Controllers
{
    [Controller]
    [Route("api/role")]
    public class RoleContoller : ControllerBase
    {
        private IRoleService roleService;

        public RoleContoller(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RoleDTO dto)
        {
            var result = await roleService.CreateAsync(dto);

            if (result)
                return Ok();

            return BadRequest("Invalid role data");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] RoleDTO dto)
        {
            var result = await roleService.UpdateAsync(dto);

            if (result)
                return Ok();

            return BadRequest("Invalid role data");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete (string id)
        {
            var result = await roleService.DeleteAsync(id);

            if (result)
                return Ok();

            return BadRequest("Invalid id");
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id required");

            var result = roleService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await roleService.GetAllAsync());
        }
    }
}