using Auth.BAL.Interface;
using Auth.DAL.Models.DbModels;
using Auth.DAL.Models.Dto.UserDto;
using Auth.DAL.Repositories.UOW;
using Auth.DAL.Repositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTAuthentication;
using AppCommonMethods;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Auth.BAL.Service
{
    public class UserService:IUserService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserRepository<User> _UserRepository;
        private UnitOfWork<User> _uowUser;
    
      
        #endregion

        #region Constructor

        public UserService(TokenService tokenService, UserRepository<User> UserRepository, UnitOfWork<User> uowUser, IMapper mapper,
           
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _UserRepository = UserRepository;
            //_UserRoleRepository = UserRoleRepository;
            _uowUser = uowUser;
          
        }

        #endregion

        #region CRUD Operations

        public async Task<CreateOrEditUserDto> CreateOrEdit(CreateOrEditUserDto input)
        {
            if (AppCommonMethods.AppCommonMethod.IsNullOrEmptyGuid(input.Id))
                return await Create(input);
            else
                return await Update(input);
        }
        private async Task<CreateOrEditUserDto> Create(CreateOrEditUserDto input)
        {
            //input.Cnic = Regex.Replace(input.Cnic, @"[^0-9]", "");

            var user = await _uowUser.Repository.GetALL(x => x.Email == input.Email || x.Username == input.Username).FirstOrDefaultAsync();

            if (user != null)
            {
                if (user.Email == input.Email)
                    throw new UserFriendlyException(CommonMessageConstant.EmailAlreadyExists);

                if (user.Username == input.Username)
                    throw new UserFriendlyException(CommonMessageConstant.CNICAlreadyExists);

                else
                    throw new UserFriendlyException(CommonMessageConstant.UserAlreadyExists);

            }

            var obj = _mapper.Map<User>(input);
            obj.Username = input.Username;

           
           

            await FillEntity(obj);

            User responseObj = await _uowUser.Repository.Insert(obj);
            await _uowUser.Save();
            return _mapper.Map<CreateOrEditUserDto>(responseObj);
        }

        private async Task<CreateOrEditUserDto> Update(CreateOrEditUserDto input)
        {

            using (var trans = _uowUser.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    

                   // var _uowUserRole = new UnitOfWork<UserRole>(_uowUser.GetDbContext());

                    //var userRoles = await _uowUserRole.Repository.GetALL(x => x.UserId == input.UserId).ToListAsync();

                    //foreach (var userRolesItem in userRoles)
                    //{
                    //    _uowUserRole.Repository.Delete(userRolesItem);
                    //    await _uowUserRole.Save();
                    //}

                    var dbObj = await _uowUser.Repository.GetById(input.Id);

                    

                   

                    dbObj.Username = input.Username;                 
                    dbObj.Email = input.Email;
                    dbObj.Password = input.Password;
                    dbObj.IsActive = input.IsActive;
                    dbObj.UpdatedBy = _tokenService.GetUserId();
                    dbObj.UpdatedOn = DateTime.Now;
                    dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;

                
                    _uowUser.Repository.Update(dbObj);
                    await _uowUser.Save();

                    //foreach (var item in input.UserRoles)
                    //{
                    //    //if(AppCommonMethod.IsNullOrEmptyGuid(item.UserRoleId)
                    //    //{
                    //    var obj = _mapper.Map<UserRole>(item);

                    //    obj.UserRoleId = Guid.NewGuid();
                    //    obj.UserId = dbObj.UserId;
                    //    obj.RoleId = item.RoleId;
                    //    obj.CreatedBy = _tokenService.GetUserId();
                    //    obj.CreatedOn = DateTime.Now;
                    //    obj.ActionTypeId = (int)ActionTypeEnum.Create;
                    //    await _uowUserRole.Repository.Insert(obj);
                    //    await _uowUserRole.Save();
                    //    //}
                    //    //else
                    //    //{
                    //    //    var userRoleDbObj = await _uowUserRole.Repository.GetById(item.UserRoleId);
                    //    //    var obj = _mapper.Map(item, userRoleDbObj);
                    //    //    obj.UpdatedBy = _tokenService.GetUserId();
                    //    //    obj.UpdatedOn = DateTime.Now;
                    //    //    obj.ActionTypeId = (int)ActionTypeEnum.Edit;
                    //    //    _uowUserRole.Repository.Update(obj);
                    //    //    await _uowUserRole.Save();
                    //    //}
                    //}

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
            return _mapper.Map<CreateOrEditUserDto>(input);
        }
        #endregion
        #region Helper Methods

        private async Task FillEntity(User obj)
        {
            //var userRoleDbList = await _UserRepository.GetALL(x => x.UserId == obj.UserId);
            if (obj.Id == null || obj.Id == Guid.Empty)
            {
                obj.Id = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId(); 
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }

            //foreach (var userRole in obj.UserRoles)
            //{
            //    if (userRole.UserRoleId == Guid.Empty)
            //    {
            //        userRole.UserRoleId = Guid.NewGuid();
            //        userRole.UserId = obj.UserId;
            //        userRole.CreatedBy = _tokenService.GetUserId();
            //        userRole.CreatedOn = DateTime.Now;
            //        userRole.ActionTypeId = (int)ActionTypeEnum.Create;
            //    }
            //    else
            //    {
            //        userRole.CreatedBy = obj.CreatedBy;
            //        userRole.CreatedOn = obj.CreatedOn;
            //        userRole.UserId = obj.UserId;
            //        userRole.UpdatedBy = _tokenService.GetUserId();
            //        userRole.UpdatedOn = DateTime.Now;
            //        userRole.ActionTypeId = (int)ActionTypeEnum.Edit;
            //    }
            //}

        }
        //private void FillEntityDelete(User obj)
        //{
        //    if (obj != null)
        //    {
        //        obj.DeletedBy = _tokenService.GetUserId();
        //        obj.DeletedOn = DateTime.Now;
        //        obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        //    }
        //}

        #endregion
    }
}
