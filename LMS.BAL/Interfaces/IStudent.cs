using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BAL.Interfaces
{
    public interface IStudent
    {
        Task<CreateOrEditStudent> CreateOrEditStudentCreate(CreateOrEditStudent input);
        Task<List<GetAllStudentsDto>> GetAllStudents(CommonListDto common = null);
        Task<ViewSingleStudentDto> GetSingleStudentForView(Guid id);
    }
}
