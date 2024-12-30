using CommonDTOs.ResponseDTO;
using LMS.BAL.Interfaces;
using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly ILibrary _library;

        #endregion

        #region Constructor
        public LibraryController(ILibrary library)
        {
            _library = library;
        }
        #endregion

        #region CU
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditLibrary input)
        {
            var obj = await _library.CreateOrEdit(input);
            return Ok(new ResponseSave
            {
                data = obj,
                message = "Record Saved Successfully.",
                statusCode = HttpStatusCode.OK
            });
        }
        #endregion

        #region GET
        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks([FromQuery] LibraryListDto common)
        {
            var list = await _library.GetAllBooks(common);
            return Ok(new ResponseSuccess
            {
                data = list
            });
        }


        [HttpGet("GetSingleBookRecord")]
        public async Task<IActionResult> GetSingleBookRecord(Guid id)
        {
            var obj = await _library.GetSingleBookRecord(id);
            return Ok(new ResponseSuccess
            {
                data = obj
            });
        }

        #endregion
    }
}
