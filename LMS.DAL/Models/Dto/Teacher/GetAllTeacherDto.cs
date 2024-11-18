using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Teacher
{
    public class GetAllTeacherDto
    {
        public Guid TeacherId { get; set; }
        public Guid UserId { get; set; }
        public string IdNo { get; set; } = null;
        public string Class { get; set; } = null;
        public string Section { get; set; } = null;
        public string TeacherPhotoBase64 { get; set; } = null;
        public string Subject { get; set; } = null;
        public decimal Salary { get; set; }
        public string Address { get; set; } = null;
        public string PhoneNo { get; set; } = null;
        public string ShortBio { get; set; } = null;
        public string FullName { get; set; } = null;
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; } = null;
        public string Gender { get; set; } = null;
        public string Email { get; set; } = null;
        public int TotalCount { get; set; } = 0;
    }
}
