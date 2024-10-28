using CommonDTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.UserDTO
{
    public class UserLoggedInfoDTO
    {
        //public UserLoggedInfoDTO()
        //{
        //    UserRoleList = new List<UserRoleDto>();
        //}
        public Guid UserId { get; set; }
       
        public string? Email { get; set; }
       // public string? ContactNo { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        
        
        //public string FormType { get; set; }
     
       // public bool IsSuperAdmin { get; set; }
        
        
       // public int UserLevel { get; set; }
      
        public string? Token { get; set; }
      //  public IList<UserRoleDto> UserRoleList { get; set; }
     
    }

    //public partial class UserRoleDto
    //{
    //    public Guid RoleId { get; set; }

    //    public string Name { get; set; } = null!;
    //    public string? ShortName { get; set; } = null!;
    //    public string? RoutingUrl { get; set; } = null!;
    //    public string? RoleType { get; set; } = null!;
    //}
}
