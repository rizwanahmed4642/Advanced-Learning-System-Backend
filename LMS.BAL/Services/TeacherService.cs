using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using JWTAuthentication;
using LMS.BAL.COMMON;
using LMS.BAL.Interfaces;
using LMS.DAL.Models.DbModels;
using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Student;
using LMS.DAL.Models.Dto.Teacher;
using LMS.DAL.Repositories._UOW;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BAL.Services
{
    public class TeacherService : ITeacher
    {
        #region Class Fields & Properties 
        private readonly UnitOfWork<Teacher> _uowTeacher;
        private IMapper _mapper;
        private readonly TokenService _tokenService;
        #endregion

        #region Constructor
        public TeacherService(UnitOfWork<Teacher> uowTeacher, IMapper mapper, TokenService tokenService)
        {
            _uowTeacher = uowTeacher;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        #endregion
        #region CU
        public async Task<CreateOrEditTeacherDto> CreateOrEditTeacherCreate(CreateOrEditTeacherDto input)
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
        public async Task<List<GetAllTeacherDto>> GetAllTeachers(TeacherListDto common)
        {
            using (var db = new AdvancedLearningSystemdbContext())
            {
                var conn = _uowTeacher.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[sp_Teacher]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Type", "GETALL");
                    sqlComm.Parameters.AddWithValue("@searchByID", common.searchByIDNo);
                    sqlComm.Parameters.AddWithValue("@searchByName", common.searchByName);
                    sqlComm.Parameters.AddWithValue("@searchByPhoneNo", common.searchByPhoneNo);
                    sqlComm.Parameters.AddWithValue("@PageNumber", common.PageNo);
                    sqlComm.Parameters.AddWithValue("@PageSize", common.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetAllTeacherDto> lst = new List<GetAllTeacherDto>();
                    if (ds.Tables.Count > 0)
                    {
                        lst = ds.Tables[0].ToList<GetAllTeacherDto>();
                    }

                    return lst;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public async Task<GetSingleTeacherDto> GetTeacherById(Guid id)
        {
            using (var db = new AdvancedLearningSystemdbContext())
            {
                try
                {
                    var conn = _uowTeacher.GetDbContext().Database.GetDbConnection();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[sp_Teacher]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Type", "GETBYID");
                    sqlComm.Parameters.AddWithValue("@Id", id);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetSingleTeacherDto> lst = ds.Tables[0].ToList<GetSingleTeacherDto>();



                    return lst[0];
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {

                }
            }
        }

        #endregion

        #region Helper Method
        private async Task<CreateOrEditTeacherDto> Create(CreateOrEditTeacherDto input)
        {
            try
            {
                var unitOfWorkUser = new UnitOfWork<User>();
                var unitOfWorkUserRole = new UnitOfWork<UserRole>();

                // Map and fill user entity
                var user = _mapper.Map<User>(input);
                FillByEntityUser(user);

                // Map and fill student entity
                var teacher = _mapper.Map<Teacher>(input.TeacherCreateOrEditDto);
                FillByEntityTeacher(teacher);

                // Insert user
                var userInsert = unitOfWorkUser.Repository.Insert(user);
                if (userInsert == null)
                {
                    throw new UserFriendlyException("Failed to add user.");
                }

                // Set UserId for student
                teacher.UserId = user.Id;

                // Insert student
                var teacherInsert = _uowTeacher.Repository.Insert(teacher);
                if (teacherInsert == null)
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
                await _uowTeacher.CommitAsync();
                await unitOfWorkUserRole.CommitAsync(); // Ensure to commit roles as well

                return input;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        private async Task<CreateOrEditTeacherDto> Update(CreateOrEditTeacherDto input)
        {
            try
            {
                var unitOfWorkUser = new UnitOfWork<User>();
                var unitOfWorkUserRole = new UnitOfWork<UserRole>();
                var userObj = await unitOfWorkUser.Repository.GetById(input.Id);
                // Map and fill user entity
                var user = _mapper.Map(input, userObj);
                FillByEntityUser(user);

                // Map and fill student entity
                var teacherObj = await _uowTeacher.Repository.GetById(input!.TeacherCreateOrEditDto!.TeacherId);
                var teacher = _mapper.Map(input.TeacherCreateOrEditDto, teacherObj);
                FillByEntityTeacher(teacher);

                // Update user
                unitOfWorkUser.Repository.Update(user);


                // Update student
                _uowTeacher.Repository.Update(teacher);

                // Commit all changes in a transaction
                await unitOfWorkUser.CommitAsync();
                await _uowTeacher.CommitAsync();

                return input;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
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
                input.Username = input.FirstName + Guid.NewGuid().ToString().Substring(0, 8);
            }
            else
            {
                input.ActionTypeId = (int)ActionTypeEnum.Edit;
                input.UpdatedOn = DateTime.Now;
                input.IsActive = true;
                input.UpdatedBy = _tokenService.GetUserId();
            }
        }

        private void FillByEntityTeacher(Teacher input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.TeacherId))
            {
                input.TeacherId = Guid.NewGuid();
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
