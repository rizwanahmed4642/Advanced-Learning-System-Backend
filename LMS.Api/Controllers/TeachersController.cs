using CommonDTOs.ResponseDTO;
using LMS.BAL.Interfaces;
using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Student;
using LMS.DAL.Models.Dto.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeachersController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly ITeacher teacher;
        #endregion

        #region Constructor
        public TeachersController(ITeacher teacher)
        {
            this.teacher = teacher;
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditTeacherDto input)
        {
            var obj = await teacher.CreateOrEditTeacherCreate(input);

            return Ok(new ResponseSuccess
            {
                statusCode = HttpStatusCode.Created,
                message = "Student Added Successfully..!"
            });
        }
        #endregion

        #region GET
        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers([FromQuery] TeacherListDto common)
        {
            var list = await teacher.GetAllTeachers(common);
            return Ok(new PaginatedResponseSuccess
            {
                data = list,
                PageNo = common.PageNo,
                PageSize = common.PageSize,
                TotalCount = list.Count > 0 ? list[0].TotalCount : 0,
            });
        }

        [HttpGet("GetTeacherById")]
        public async Task<IActionResult> GetTeacherById([FromQuery] Guid id)
        {
            var obj = await teacher.GetTeacherById(id);
            return Ok(new ResponseSuccess
            {
                data = obj
            });
        }
        #endregion
    }
}
