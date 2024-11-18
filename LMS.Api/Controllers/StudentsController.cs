using CommonDTOs.ResponseDTO;
using LMS.BAL.Interfaces;
using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly IStudent student;

        #endregion

        #region Constructor
        public StudentsController(IStudent student)
        {
            this.student = student;
        }
        #endregion

        #region GET
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents([FromQuery] StudentListDto common) 
        {
            var list = await student.GetAllStudents(common);

            return Ok(new PaginatedResponseSuccess
            {
                data = list,
                PageNo = common.PageNo,
                PageSize = common.PageSize,
                TotalCount = list.Count > 0 ? list[0].TotalCount : 0,
            });
        }
        
        [HttpGet("GetSingleStudentForView")]
        public async Task<IActionResult> GetSingleStudentForView([FromQuery] Guid id) 
        {
            var obj = await student.GetSingleStudentForView(id);

            return Ok(new ResponseSuccess
            {
                data = obj
            });
        }

        [HttpGet("GetStudentsById")]
        public async Task<IActionResult> GetStudentsById([FromQuery] Guid id)
        {
            var obj = await student.GetStudentsById(id);
            return Ok(new ResponseSuccess
            {
                data = obj
            });
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditStudent input) 
        { 
            var obj = await student.CreateOrEditStudentCreate(input);
            return Ok(new ResponseSuccess
            {
                statusCode = HttpStatusCode.Created,
                message = "Student Added Successfully..!"
            });
        }
        #endregion

        #region DELETE
        [HttpPost("DELETESTUDENTRECORD")]
        public async Task<IActionResult> DELETESTUDENTRECORD([FromQuery] Guid id)
        {
            var msg = await student.DELETESTUDENTRECORD(id);
            return Ok(new ResponseDelete
            {
                message = msg
            });
        }
        #endregion

        #region Helper Method

        #endregion
    }
}
