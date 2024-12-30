using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Common
{
    public class StudentListDto : PagerDto
    {
        public string? searchByName { get; set; }
        public string? searchByClass { get; set; }
        public string? searchByRollNo { get; set; }
    }

    public class TeacherListDto : PagerDto
    {
        public string? searchByName { get; set; }
        public string? searchByPhoneNo { get; set; }
        public string? searchByIDNo { get; set; }
    }

    public class ParentListDto : PagerDto
    {
        public string? searchByIdNo { get; set; }
        public string? searchByName { get; set; }
        public string? searchByPhoneNo { get; set; }
    }
    
    public class LibraryListDto : PagerDto
    {
        public string? searchByIdNo { get; set; }
        public string? searchByBookName { get; set; }
        public string? searchByWriterName { get; set; }
    }
}
