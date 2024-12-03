using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Parent
{
    public class CreateOrEditParent
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? GenderTypeProfileId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? RoleShortName { get; set; }
        public ParentCreateOrEditDto? ParentCreateOrEditDto { get; set; }
    }

    public class ParentCreateOrEditDto
    {
        public Guid? ParentId { get; set; }
        public string Occupation { get; set; } = null!;
        public string Idno { get; set; } = null!;
        public string MotherName { get; set; } = null!;
        public Guid BloodGroupTypeProfileId { get; set; }
        public Guid ReligionTypeProfileId { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string? ShortBio { get; set; }
        public string ParentPhotoBase64 { get; set; } = null!;
    }
}
