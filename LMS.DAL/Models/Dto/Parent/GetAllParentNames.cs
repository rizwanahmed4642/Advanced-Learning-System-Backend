using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Models.Dto.Parent
{
    public class GetAllParentNames
    {
        public Guid ParentId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
