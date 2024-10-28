using Auth.BAL.Interface;
using Auth.DAL.Models.DbModels;
using Auth.DAL.Models.Dto.UserDto;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly IUserService _UserService;
       

        #endregion

        #region Constructor

        public UserController(ILogger<AuthenticationController> logger, TokenService tokenService, IUserService UserService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _UserService = UserService;
        }

        #endregion
        #region CRUD Operations
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditUserDto input)
        {
            var obj = await _UserService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        #endregion
    }
}
