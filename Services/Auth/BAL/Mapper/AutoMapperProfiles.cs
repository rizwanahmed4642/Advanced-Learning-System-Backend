
using autoMapper = AutoMapper;

using CommonDTOs.LocationDTO;
using CommonDTOs.DropdownDTO;
using Auth.DAL.Models.DbModels;
using Auth.DAL.Models.Dto.UserDto;


namespace AuthDAL
{
    public class AutoMapperProfiles
    {
        #region UMS
        public class UserProfile : autoMapper.Profile
        {
            public UserProfile()
            {
                CreateMap<User, CreateOrEditUserDto>().ReverseMap();
                CreateMap<UserRole, CreateOrEditUserRoleDto>().ReverseMap();

            }
        }

        

        #endregion

        

    }
}
