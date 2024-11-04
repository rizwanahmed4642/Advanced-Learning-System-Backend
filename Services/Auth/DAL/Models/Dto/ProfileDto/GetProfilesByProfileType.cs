using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DAL.Models.Dto.ProfileDto
{
    public class GetProfilesByProfileType
    {
        public Guid ProfileTypeId { get; set; }
        public string ProfileTypeName { get; set; } = string.Empty;
        public string ProfileTypeShortName { get; set; } = string.Empty;
        public Guid? ParentProfileTypeId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid? ParentProfileId { get; set; }
        public string ProfileName { get; set; } = string.Empty;
        public string ProfileShortName { get; set; } = string.Empty;   
        public DateTime CreatedOn { get; set; }

    }
}
