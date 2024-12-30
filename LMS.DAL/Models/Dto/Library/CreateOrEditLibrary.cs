using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Library
{
    public class CreateOrEditLibrary
    {
        public Guid? LibraryId { get; set; }
        public string BookName { get; set; } = null!;
        public Guid SubjectTypeProfileId { get; set; }
        public string WriterName { get; set; } = null!;
        public Guid ClassTypeProfileId { get; set; }
        public string? IdNo { get; set; }
        public DateTime PublishingDate { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
