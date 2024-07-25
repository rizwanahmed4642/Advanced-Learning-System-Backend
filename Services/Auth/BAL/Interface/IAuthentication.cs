using DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IAuthentication
    {
        public async Task<UserLoggedInfoDTO> Authenticate(UserLoginDTO userLoginDTO);
    }
}
