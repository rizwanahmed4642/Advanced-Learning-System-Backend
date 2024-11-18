using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Student;
using LMS.DAL.Models.Dto.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BAL.Interfaces
{
    public interface ITeacher
    {
        Task<CreateOrEditTeacherDto> CreateOrEditTeacherCreate(CreateOrEditTeacherDto input);
        Task<List<GetAllTeacherDto>> GetAllTeachers(TeacherListDto common = null);
        Task<GetSingleTeacherDto> GetTeacherById(Guid id);
    }
}
