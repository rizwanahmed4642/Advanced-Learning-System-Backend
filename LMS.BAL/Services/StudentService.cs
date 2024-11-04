using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using LMS.BAL.COMMON;
using LMS.BAL.Interfaces;
using LMS.DAL.Models.DbModels;
using LMS.DAL.Models.Dto.Student;
using LMS.DAL.Repositories._UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BAL.Services
{
    public class StudentService : IStudent
    {
        #region Class Fields & Properties
        private readonly UnitOfWork<Student> _uowStudent;
        private IMapper _mapper;
        private readonly TokenService _tokenService;
        #endregion

        #region Constructor
        public StudentService(UnitOfWork<Student> uowStudent, IMapper mapper, TokenService tokenService)
        {
            _uowStudent = uowStudent;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        #endregion

        #region POST
        public async Task<CreateOrEditStudent> CreateOrEditStudentCreate(CreateOrEditStudent input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.Id)) 
            {
                return await Create(input);  
            } 
            else
            {
                return await Update(input);
            }
        }
        #endregion

        #region GET

        #endregion

        #region Helper Method
        private async Task<CreateOrEditStudent> Create(CreateOrEditStudent input)
        {
            var unitOfWorkUser = new UnitOfWork<User>();
            var unitOfWorkUserRole = new UnitOfWork<UserRole>();

            // Map and fill user entity
            var user = _mapper.Map<User>(input);
            FillByEntityUser(user);

            // Map and fill student entity
            var student = _mapper.Map<Student>(input.StudentCreateOrEditDto);
            FillByEntityStudent(student);

            // Insert user
            var userInsert = unitOfWorkUser.Repository.Insert(user);
            if (userInsert == null)
            {
                throw new UserFriendlyException("Failed to add user.");
            }

            // Set UserId for student
            student.UserId = user.Id;

            // Insert student
            var studentInsert = _uowStudent.Repository.Insert(student);
            if (studentInsert == null)
            {
                throw new UserFriendlyException("Failed to add student.");
            }

            // Find role for user
            var userRole = await unitOfWorkUserRole.GetDbContext().Roles
                .Where(x => x.ShortName!.ToLower() == input.RoleShortName!.ToLower())
                .FirstOrDefaultAsync();

            if (userRole == null)
            {
                throw new UserFriendlyException("Role not found.");
            }

            // Create new user role
            var newUserRole = new UserRole
            {
                UserId = (Guid)user.Id,
                RoleId = userRole.RoleId,
                IsActive = true,
                CreatedBy = _tokenService.GetUserId(),
                CreatedOn = DateTime.Now,
                ActionTypeId = (int)ActionTypeEnum.Create
            };

            // Insert user role
            var insertUserRole = unitOfWorkUserRole.Repository.Insert(newUserRole);
            if (insertUserRole == null)
            {
                throw new UserFriendlyException("Failed to assign role to user.");
            }

            // Commit all changes in a transaction
            await unitOfWorkUser.CommitAsync();
            await _uowStudent.CommitAsync();
            await unitOfWorkUserRole.CommitAsync(); // Ensure to commit roles as well

            return input;
        }

        private Task<CreateOrEditStudent> Update(CreateOrEditStudent input)
        {
            throw new NotImplementedException();
        }

        private void FillByEntityUser(User input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.Id))
            {
                input.Id = Guid.NewGuid();
                input.ActionTypeId = (int)ActionTypeEnum.Create;
                input.CreatedBy = _tokenService.GetUserId();
                input.CreatedOn = DateTime.Now;
                input.IsActive = true;
                input.Password = PasswordGenerator.GeneratePassword(10, true, true, true, true);
                input.Username = input.FirstName + Guid.NewGuid().ToString().Substring(0,8);
            } 
            else
            {
                input.ActionTypeId = (int)ActionTypeEnum.Edit;
                input.UpdatedOn = DateTime.Now;
                input.IsActive = true;
                input.UpdatedBy = _tokenService.GetUserId();
            }
        }

        private void FillByEntityStudent(Student input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.StudentId))
            {
                input.StudentId = Guid.NewGuid();
                input.ActionTypeId = (int)ActionTypeEnum.Create;
                input.CreatedBy = _tokenService.GetUserId();
                input.CreatedOn = DateTime.Now;
                input.IsActive = true;
            }
            else
            {
                input.ActionTypeId = (int)ActionTypeEnum.Edit;
                input.UpdatedOn = DateTime.Now;
                input.IsActive = true;
                input.UpdatedBy = _tokenService.GetUserId();
            }
        }
        #endregion
    }
}
