using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Teacher
{
    public class GetSingleTeacherDto
    {
        public Guid TeacherId { get; set; }
        public Guid UserId { get; set; }
        public string IdNo { get; set; } = null;
        public Guid BloodGroupTypeProfileId { get; set; }
        public Guid ReligionTypeProfileId { get; set; }
        public Guid ClassTypeProfileId { get; set; }
        public Guid SectionTypeProfileId { get; set; }
        public Guid SubjectTypeProfileId { get; set; }
        public Guid GenderTypeProfileId { get; set; }
        public string TeacherPhotoBase64 { get; set; } = null;
        public decimal Salary { get; set; }
        public string Address { get; set; } = null;
        public string PhoneNo { get; set; } = null;
        public string ShortBio { get; set; } = null;
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;
        public string Username { get; set; } = null;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null;
    }
}
