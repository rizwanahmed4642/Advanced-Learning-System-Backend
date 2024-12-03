using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Student
{
    public class ViewSingleStudentDto
    {
        public Guid UserId { get; set; }
        public string LastName { get; set; } = null;
        public string FirstName { get; set; } = null;
        public string Email { get; set; } = null;
        public string FatherName { get; set; } = null;
        public string MotherName { get; set; } = null;
        public string Address { get; set; } = null;
        public string Occupation { get; set; } = null;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null;
        public string Religion { get; set; } = null;
        public DateTime AdmissionDate { get; set; }
        public string AdmissionId { get; set; } = null;
        public string StudentClass { get; set; } = null;
        public string StudentSection { get; set; } = null;
        public string RollNo { get; set; } = null;
        public string PhoneNo { get; set; } = null;
        public string ShortBio { get; set; } = null;
        public string StudentPhotoBase64 { get; set; } = null;
    }
}
