using AppCommonMethods;
using BAL.Interface;
using CommonMessages;
using AppCommonMethods;
using AppCommonMethods.AppConstants;
using Auth.DAL.Models.DbModels;
using Auth.DAL.Repositories.UOW;
using DTOs.UserDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExceptionHandler;
using Auth.DAL.Repositories;
using AutoMapper;
using JWTAuthentication;
using Microsoft.Extensions.Configuration;

namespace Auth.BAL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserRepository<User> _UserRepository;
        private UnitOfWork<User> _uowUser;


        #endregion
        #region Constructor
        public AuthenticationService(TokenService tokenService, UserRepository<User> UserRepository, UnitOfWork<User> uowUser, IMapper mapper,

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
        public async Task<UserLoggedInfoDTO> Authenticate(UserLoginDTO userLoginDTO)
        {
            UserLoggedInfoDTO userObj = new UserLoggedInfoDTO();
            if (userLoginDTO != null)
            {
                var user = await _uowUser.Repository.GetALL(x => x.Username == userLoginDTO.Username).FirstOrDefaultAsync();
                if (AppCommonMethod.IsNullObject(user))
                    throw new UserFriendlyException(CommonMessageConstant.UserNotFound);
                else if (!user!.Password.Equals(userLoginDTO.Password))
                    throw new UserFriendlyException(CommonMessageConstant.IncorrectPassword);

                userObj.UserId = user.Id;
                userObj.Username = user.Username;
                userObj.Email = user.Email;
                userObj.Password = user.Password;
            }
            userObj.Token = TokenService.GenerateSecurityToken(userObj);
            return await Task.FromResult(userObj);

        }
    }
}
