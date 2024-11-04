using Auth.DAL.Models.Dto.ProfileDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.BAL.Interface
{
    public interface IProfileService
    {
        Task<List<GetProfilesByProfileType>> GetProfilesByProfileTypes(string shortName);
    }
}
