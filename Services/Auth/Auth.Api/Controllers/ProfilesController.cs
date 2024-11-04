using Auth.BAL.Interface;
using CommonDTOs.ResponseDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly IProfileService profileService;
        #endregion

        #region Constructor
        public ProfilesController(IProfileService profileService)
        {
            this.profileService = profileService;
        }
        #endregion

        #region GET
        [HttpGet]
        [Route("GetProfilesByProfileTypes")]
        public async Task<IActionResult> GetProfilesByProfileTypes(string shortName)
        {
            var list = await profileService.GetProfilesByProfileTypes(shortName);
            return Ok(new ResponseSuccess
            {
                data = list,
                statusCode = HttpStatusCode.OK
            });
        }
        #endregion
    }
}
