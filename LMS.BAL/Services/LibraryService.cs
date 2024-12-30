using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using JWTAuthentication;
using LMS.BAL.COMMON;
using LMS.BAL.Interfaces;
using LMS.DAL.Models.DbModels;
using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Library;
using LMS.DAL.Models.Dto.Parent;
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
    public class LibraryService: ILibrary
    {
        #region Class Fields & Properties
        private IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly UnitOfWork<Library> _unitOfWorkLibrary;
        #endregion

        #region Constructor
        public LibraryService(IMapper mapper, TokenService tokenService, UnitOfWork<Library> unitOfWork)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _unitOfWorkLibrary = unitOfWork;
        }
        #endregion

        #region CU
        public async Task<CreateOrEditLibrary> CreateOrEdit(CreateOrEditLibrary library)
        {
            if (AppCommonMethod.IsNullObject(library)) 
            {
                throw new UserFriendlyException("Please Fill the required Fields");
            }

            if (AppCommonMethod.IsNullOrEmptyGuid(library.LibraryId))
            {
                return await Create(library);
            }
            else
            {
                return await Update(library);
            }
        }
        #endregion

        #region GET
        public async Task<List<GetAllBooksDto>> GetAllBooks(LibraryListDto common)
        {
            using (var db = new AdvancedLearningSystemdbContext())
            {
                var conn = _unitOfWorkLibrary.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[sp_Library]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Type", "GETALL");
                    sqlComm.Parameters.AddWithValue("@searchByIdNo", common.searchByIdNo);
                    sqlComm.Parameters.AddWithValue("@searchByBookName", common.searchByBookName);
                    sqlComm.Parameters.AddWithValue("@searchByWriterName", common.searchByWriterName);
                    sqlComm.Parameters.AddWithValue("@PageNumber", common.PageNo);
                    sqlComm.Parameters.AddWithValue("@PageSize", common.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetAllBooksDto> lst = new List<GetAllBooksDto>();
                    if (ds.Tables.Count > 0)
                    {
                        lst = ds.Tables[0].ToList<GetAllBooksDto>();
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

        public async Task<CreateOrEditLibrary> GetSingleBookRecord(Guid id)
        {
            using (var db = new AdvancedLearningSystemdbContext())
            {
                try
                {
                    var conn = _unitOfWorkLibrary.GetDbContext().Database.GetDbConnection();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[sp_Library]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Type", "GETBYID");
                    sqlComm.Parameters.AddWithValue("@Id", id);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<CreateOrEditLibrary> lst = ds.Tables[0].ToList<CreateOrEditLibrary>();



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

        #region Helper Methdod

        private async Task<CreateOrEditLibrary> Create(CreateOrEditLibrary input)
        {
            try
            {
                var library = _mapper.Map<Library>(input);
                FillByEntity(library);
                
                await _unitOfWorkLibrary.Repository.Insert(library);
                await _unitOfWorkLibrary.CommitAsync();

                return input;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        private async Task<CreateOrEditLibrary> Update(CreateOrEditLibrary input)
        {
            try
            {
                var userObj = await _unitOfWorkLibrary.Repository.GetById(input.LibraryId);
                // Map and fill user entity
                var library = _mapper.Map(input, userObj);
                FillByEntity(library);


                // Update user
                _unitOfWorkLibrary.Repository.Update(library);
                await _unitOfWorkLibrary.CommitAsync();

                return input;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        private void FillByEntity(Library input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.LibraryId))
            {
                input.LibraryId = Guid.NewGuid();
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
