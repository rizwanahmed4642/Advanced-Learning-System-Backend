using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Library
{
    public class GetAllBooksDto
    {
        public Guid LibraryId { get; set; }
        public string BookName { get; set; }
        public string WriterName { get; set; }
        public string IdNo { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public DateTime PublishingDate { get; set; }
        public DateTime UploadDate { get; set; }
        public int TotalCount { get; set; } 
    }
}
