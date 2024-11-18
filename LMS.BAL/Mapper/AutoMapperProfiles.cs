using LMS.DAL.Models.DbModels;
using LMS.DAL.Models.Dto.Student;
using LMS.DAL.Models.Dto.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BAL.Mapper
{
    public class AutoMapperProfiles
    {
        #region Student
        public class StudentProfile : AutoMapper.Profile 
        {
            public StudentProfile()
            {
                CreateMap<Student, StudentCreateOrEditDto>().ReverseMap();
                CreateMap<User, CreateOrEditStudent>().ReverseMap();
            }
        }

        public class TeacherProfile : AutoMapper.Profile
        {
            public TeacherProfile()
            {
                CreateMap<Teacher, TeacherCreateOrEditDto>().ReverseMap();
                CreateMap<User, CreateOrEditTeacherDto>().ReverseMap();
            }
        }
        #endregion
    }
}
