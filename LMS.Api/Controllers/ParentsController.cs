using CommonDTOs.ResponseDTO;
using LMS.BAL.Interfaces;
using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Parent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParentsController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly IParent _parent;
        #endregion

        #region Consturtor
        public ParentsController(IParent parent)
        {
            _parent = parent;
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditParent input)
        {
            var obj = await _parent.CreateOrEditParentCreate(input);
            return Ok(new ResponseSave
            {
                data = obj
            });
        }
        #endregion

        #region GET
        [HttpGet("GetAllParents")]
        public async Task<IActionResult> GetAllTeachers([FromQuery] ParentListDto common)
        {
            var list = await _parent.GetAllParents(common);
            return Ok(new PaginatedResponseSuccess
            {
                data = list,
                PageNo = common.PageNo,
                PageSize = common.PageSize,
                TotalCount = list.Count > 0 ? list[0].TotalCount : 0,
            });
        }
        [HttpGet("GetSingleParentForView")]
        public async Task<IActionResult> GetSingleParentForView([FromQuery] Guid id)
        {
            var obj = await _parent.GetSingleParentForView(id);
            return Ok(new ResponseSuccess
            {
                data = obj
            });
        }

        [HttpGet("GetSingleparentRecord")]
        public async Task<IActionResult> GetSingleparentRecord([FromQuery] Guid id)
        {
            var obj = await _parent.GetSingleparentRecord(id);
            return Ok(new ResponseSuccess
            {
                data = obj
            });
        }

        [HttpGet("GetParentName")]
        public async Task<IActionResult> GetParentName()
        {
            var list = await _parent.GetParentName();
            return Ok(new ResponseSuccess
            {
                data = list
            });
        }
        #endregion
    }
}
