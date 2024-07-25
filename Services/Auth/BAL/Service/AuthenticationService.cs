using BAL.Interface;
using DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service
{
    public class AuthenticationService : IAuthentication
    {
        public async Task<UserLoggedInfoDTO> Authenticate(UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO == null)
            {

            }
        }
    }
}
