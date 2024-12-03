using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Parent;
using LMS.DAL.Models.Dto.Student;
using LMS.DAL.Models.Dto.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BAL.Interfaces
{
    public interface IParent
    {
        Task<CreateOrEditParent> CreateOrEditParentCreate(CreateOrEditParent input);
        Task<List<GetAllParentsDto>> GetAllParents(ParentListDto common);
        Task<ViewSingleParentRecordDto> GetSingleParentForView(Guid id);
        Task<GetSingleParentDto> GetSingleparentRecord(Guid id);
        Task<List<GetAllParentNames>> GetParentName();
    }
}
