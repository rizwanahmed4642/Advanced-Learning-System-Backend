using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Parent
{
    public class GetSingleParentDto
    {
        public Guid ParentId { get; set; }
        public Guid Id { get; set; }
        public string? Occupation { get; set; }
        public string? IDNo { get; set; }
        public Guid BloodGroupTypeProfileId { get; set; }
        public Guid ReligionTypeProfileId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? ShortBio { get; set; }
        public string? ParentPhotoBase64 { get; set; }
        public string MotherName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public Guid GenderTypeProfileId  { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
    }
}
