using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Student
{
    public class GetSingleStudentDto
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public Guid? GenderTypeProfileId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Email { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? ParentId { get; set; }
        public string RollNo { get; set; } = null!;
        public Guid BloodGroupTypeProfileId { get; set; }
        public Guid ReligionTypeProfileId { get; set; }
        public Guid StudentClassTypeProfileId { get; set; }
        public Guid StudentClassSectionTypeProfileId { get; set; }
        public string? AdmissionId { get; set; }
        public string? PhoneNo { get; set; }
        public string? ShortBio { get; set; }
        public string? StudentPhotoBase64 { get; set; } = null!;
    }
}
