using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Common
{
    public class CommonListDto
    {
        public string? searchByName { get; set; }
        public string? searchByClass { get; set; }
        public string? searchByRollNo { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
