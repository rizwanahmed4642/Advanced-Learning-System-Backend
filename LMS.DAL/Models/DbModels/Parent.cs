using System;
using System.Collections.Generic;

namespace LMS.DAL.Models.DbModels
{
    public partial class Parent
    {
        public Guid ParentId { get; set; }
        public Guid FatherId { get; set; }
        public string MotherName { get; set; } = null!;
        public string Occupation { get; set; } = null!;
        public string Idno { get; set; } = null!;
        public Guid BloodGroupTypeProfileId { get; set; }
        public Guid ReligionTypeProfileId { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string? ShortBio { get; set; }
        public string ParentPhotoBase64 { get; set; } = null!;
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? ActionTypeId { get; set; }

        public virtual User Father { get; set; } = null!;
    }
}
