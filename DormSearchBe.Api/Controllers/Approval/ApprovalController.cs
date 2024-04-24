using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Approval;
using Microsoft.AspNetCore.Mvc;

namespace DormSearchBe.Api.Controllers.Approval
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController :ControllerBase
    {
        private readonly IApprovalService _approvalService;
        public ApprovalController(IApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            return Ok(_approvalService.Items(query));
        }


        [HttpPost]
        public IActionResult Create(ApprovalDto dto)
        {
            return Ok(_approvalService.Create(dto));
        }

        [HttpPut("{id}")]
        public IActionResult Update(ApprovalDto dto, string id)
        {
            return Ok(_approvalService.Update(dto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(_approvalService?.Delete(id));
        }
    }
}
