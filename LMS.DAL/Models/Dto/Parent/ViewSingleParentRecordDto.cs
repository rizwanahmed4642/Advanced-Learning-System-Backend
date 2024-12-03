using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Parent
{
    public class ViewSingleParentRecordDto
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Religion { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? IDNo { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? Occupation { get; set; }
        public string? ShortBio { get; set; }
        public string? ParentPhotoBase64 { get; set; }
    }
}
