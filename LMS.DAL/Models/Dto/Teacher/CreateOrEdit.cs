using LMS.DAL.Models.Dto.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Teacher
{
    public class CreateOrEditTeacherDto
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? GenderTypeProfileId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? RoleShortName { get; set; }
        public TeacherCreateOrEditDto? TeacherCreateOrEditDto { get; set; }
    }

    public class TeacherCreateOrEditDto
    {
        public Guid? TeacherId { get; set; }
        public string IdNo { get; set; } = null!;
        public Guid BloodGroupTypeProfileId { get; set; }
        public Guid ReligionTypeProfileId { get; set; }
        public Guid ClassTypeProfileId { get; set; }
        public Guid SectionTypeProfileId { get; set; }
        public Guid SubjectTypeProfileId { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string ShortBio { get; set; } = null!;
        public string TeacherPhotoBase64 { get; set; } = null!;
    }
}
