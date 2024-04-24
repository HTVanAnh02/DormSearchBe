using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Role;
using DormSearchBe.Infrastructure.Common.Utilities;
using DormSearchBe.Infrastructure.Enum;
using Microsoft.AspNetCore.Mvc;

namespace DormSearchBe.Api.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
       /* [HasApprovalAttribute(Approval.Modify)]*/

        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            return Ok(_roleService.Items(query));
        }

        [HttpPost]
        public IActionResult Create(RoleDto dto)
        {
            return Ok(_roleService.Create(dto));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(RoleDto dto)
        {
            return Ok(_roleService.Update(dto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_roleService?.Delete(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_roleService.GetById(id));
        }
    }
}
