using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using JWTAuthentication;
using LMS.BAL.COMMON;
using LMS.BAL.Interfaces;
using LMS.DAL.Models.DbModels;
using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Parent;
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
    public class ParentService : IParent
    {
        #region Class Fields & Properties
        private IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly UnitOfWork<Parent> _unitOfWorkParent;
        #endregion

        #region Constuctor
        public ParentService(IMapper mapper, TokenService tokenService, UnitOfWork<Parent> unitOfWorkParent)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _unitOfWorkParent = unitOfWorkParent;
        }
        #endregion

        #region POST
        public async Task<CreateOrEditParent> CreateOrEditParentCreate(CreateOrEditParent input)
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
        public async Task<List<GetAllParentsDto>> GetAllParents(ParentListDto common)
        {
            using (var db = new AdvancedLearningSystemdbContext())
            {
                var conn = _unitOfWorkParent.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[sp_Parents]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Type", "GETALL");
                    sqlComm.Parameters.AddWithValue("@searchByIdNo", common.searchByIdNo);
                    sqlComm.Parameters.AddWithValue("@searchByName", common.searchByName);
                    sqlComm.Parameters.AddWithValue("@searchByPhoneNo", common.searchByPhoneNo);
                    sqlComm.Parameters.AddWithValue("@PageNumber", common.PageNo);
                    sqlComm.Parameters.AddWithValue("@PageSize", common.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetAllParentsDto> lst = new List<GetAllParentsDto>();
                    if (ds.Tables.Count > 0)
                    {
                        lst = ds.Tables[0].ToList<GetAllParentsDto>();
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

        public async Task<ViewSingleParentRecordDto> GetSingleParentForView(Guid id)
        {
            using (var db = new AdvancedLearningSystemdbContext())
            {
                var conn = _unitOfWorkParent.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[sp_Parents]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Type", "View");
                    sqlComm.Parameters.AddWithValue("@Id", id);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ViewSingleParentRecordDto> lst = ds.Tables[0].ToList<ViewSingleParentRecordDto>();



                    return lst[0];
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

        public async Task<GetSingleParentDto> GetSingleparentRecord(Guid id)
        {
            using (var db = new AdvancedLearningSystemdbContext())
            {
                try
                {
                    var conn = _unitOfWorkParent.GetDbContext().Database.GetDbConnection();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[sp_Parents]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Type", "GETBYID");
                    sqlComm.Parameters.AddWithValue("@Id", id);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetSingleParentDto> lst = ds.Tables[0].ToList<GetSingleParentDto>();



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

        public async Task<List<GetAllParentNames>> GetParentName()
        {
            var list = await _unitOfWorkParent.GetDbContext()
                    .Parents
                    .Include(x => x.Father)
                    .Where(x => x.ActionTypeId != 3 && x.IsActive == true)
                    .Select(x => new GetAllParentNames()
                    {
                        ParentId = x.ParentId,
                        Name = x.Father.FirstName + " " + x.Father.LastName,
                    }).ToListAsync();
            return list;
        }
        #endregion

        #region Helper Method
        private async Task<CreateOrEditParent> Create(CreateOrEditParent input)
        {
            try
            {
                var unitOfWorkUser = new UnitOfWork<User>();
                var unitOfWorkUserRole = new UnitOfWork<UserRole>();

                // Map and fill user entity
                var user = _mapper.Map<User>(input);
                FillByEntityUser(user);

                // Map and fill student entity
                var parent = _mapper.Map<Parent>(input.ParentCreateOrEditDto);
                FillByEntityParent(parent);

                // Insert user
                var userInsert = unitOfWorkUser.Repository.Insert(user);
                if (userInsert == null)
                {
                    throw new UserFriendlyException("Failed to add user.");
                }

                // Set UserId for student
                parent.FatherId = user.Id;

                // Insert student
                var studentInsert = _unitOfWorkParent.Repository.Insert(parent);
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
                await _unitOfWorkParent.CommitAsync();
                await unitOfWorkUserRole.CommitAsync(); // Ensure to commit roles as well

                return input;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        private async Task<CreateOrEditParent> Update(CreateOrEditParent input)
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
                var studentObj = await _unitOfWorkParent.Repository.GetById(input!.ParentCreateOrEditDto!.ParentId);
                var student = _mapper.Map(input.ParentCreateOrEditDto, studentObj);
                FillByEntityParent(student);

                // Update user
                unitOfWorkUser.Repository.Update(user);


                // Update student
                _unitOfWorkParent.Repository.Update(student);

                // Commit all changes in a transaction
                await unitOfWorkUser.CommitAsync();
                await _unitOfWorkParent.CommitAsync();

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

        private void FillByEntityParent(Parent input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.ParentId))
            {
                input.ParentId = Guid.NewGuid();
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
