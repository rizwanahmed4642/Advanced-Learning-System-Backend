using System;
using System.Collections.Generic;

namespace LMS.DAL.Models.DbModels
{
    public partial class Teacher
    {
        public Guid TeacherId { get; set; }
        public Guid UserId { get; set; }
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
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? ActionTypeId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
