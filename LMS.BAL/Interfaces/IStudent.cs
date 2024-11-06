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
        Task<List<GetAllStudentsDto>> GetAllStudents(string searchTerm = null);
    }
}
