using DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IAuthenticationService
    {
      public  Task<UserLoggedInfoDTO> Authenticate(UserLoginDTO userLoginDTO);
    }
}
