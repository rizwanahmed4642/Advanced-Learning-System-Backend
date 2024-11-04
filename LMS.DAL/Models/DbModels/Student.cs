using System;
using System.Collections.Generic;

namespace LMS.DAL.Models.DbModels
{
    public partial class Student
    {
        public Guid StudentId { get; set; }
        public Guid UserId { get; set; }
        public string RollNo { get; set; } = null!;
        public Guid BloodGroupTypeProfileId { get; set; }
        public Guid ReligionTypeProfileId { get; set; }
        public string StudentClass { get; set; } = null!;
        public string Section { get; set; } = null!;
        public string? AdmissionId { get; set; }
        public string? PhoneNo { get; set; }
        public string? ShortBio { get; set; }
        public string StudentPhotoBase64 { get; set; } = null!;
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? ActionTypeId { get; set; }
    }
}
