using LMS.DAL.Models.DbModels;
using LMS.DAL.Models.Dto.Library;
using LMS.DAL.Models.Dto.Parent;
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

        public class ParentProfile : AutoMapper.Profile 
        {
            public ParentProfile()
            {
                CreateMap<User, CreateOrEditParent>().ReverseMap();
                CreateMap<Parent, ParentCreateOrEditDto>().ReverseMap();
            }
        }
        public class LibraryProfile : AutoMapper.Profile 
        {
            public LibraryProfile()
            {
                CreateMap<Library, CreateOrEditLibrary>().ReverseMap();
            }
        }
        #endregion
    }
}
