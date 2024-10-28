using Auth.DAL.Models.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.BAL.Interface
{
    public interface IUserService
    {
         Task<CreateOrEditUserDto> CreateOrEdit(CreateOrEditUserDto input);
    }
}
