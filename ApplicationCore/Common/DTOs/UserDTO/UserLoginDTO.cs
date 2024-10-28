using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.UserDTO
{
    public class UserLoginDTO
    {
        public string? Email { get; set; }
        [Required]
        public string? Username { get; set; }

       
        [Required]
        public string? Password { get; set; }
    }

    public class UserSignoutDto
    {
        public long UserId { get; set; }
    }
}
