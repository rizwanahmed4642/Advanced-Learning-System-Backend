using AppCommonMethods;
using BAL.Interface;
using CommonDTOs.ResponseDTO;
using CommonMessages;
using DTOs.UserDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly IApplicationBuilder _app;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly IAuthenticationService _authService;
        private readonly string ApiUrl;
        private readonly IHostEnvironment _env;
        private readonly string Environment;

        #endregion
        #region Constructor

        public AuthenticationController(ILogger<AuthenticationController> logger, TokenService tokenService,
              IAuthenticationService authService, 
            IConfiguration config, IHostEnvironment env)
        {
            Environment = config.GetSection("Environment").Value ?? string.Empty;
            _env = env;
            _logger = logger;
            _tokenService = tokenService ?? throw new ArgumentNullException();
            _authService = authService ?? throw new ArgumentNullException();

            //_logger.Log(LogLevel.Information, "Build Running In Environment : " + Environment);
        }

        #endregion
        #region Authentication Methods
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(UserLoginDTO input)
        {

             var obj = await _authService.Authenticate(input);            




            return Ok(new ResponseSuccess { message = CommonMessageConstant.Authenticated, data = obj });

        }

        #endregion
    }
}
