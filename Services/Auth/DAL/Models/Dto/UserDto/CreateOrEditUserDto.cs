


namespace Auth.DAL.Models.Dto.UserDto
{
    public class CreateOrEditUserDto
    {
      

        public Guid? Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool? IsActive { get; set; }

        //public virtual ICollection<CreateOrEditUserRoleDto> UserRoles { get; set; } = new List<CreateOrEditUserRoleDto>();
    }
}
