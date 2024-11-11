using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Student
{
    public class GetAllStudentsDto
    {
        public Guid StudentId { get; set; }
        public Guid UserId { get; set; }
        public string AdmissionId { get; set; } = null;
        public string PhoneNo { get; set; } = null;
        public string RollNo { get; set; } = null;
        public string StudentPhotoBase64 { get; set; } = null;
        public string Username { get; set; } = null;
        public string FullName { get; set; } = null;
        public string Gender { get; set; } = null;
        public string BloodGroup { get; set; } = null;
        public string Religion { get; set; } = null;
        public string StudentClass { get; set; } = null;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null;
        public string Section { get; set; } = null;
        public int TotalCount  { get; set; } = 0;
    }
}
