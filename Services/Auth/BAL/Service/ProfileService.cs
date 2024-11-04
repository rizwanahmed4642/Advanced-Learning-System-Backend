using Auth.BAL.Interface;
using Auth.DAL.Models.DbModels;
using Auth.DAL.Models.Dto.ProfileDto;
using Auth.DAL.Repositories;
using Auth.DAL.Repositories.UOW;
using AutoMapper;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.BAL.Service
{
    public class ProfilesService : IProfileService
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        private UnitOfWork<DAL.Models.DbModels.Profile> _uowProfile;
        #endregion

        #region Constructor
        public ProfilesService(TokenService tokenService, IMapper mapper, UnitOfWork<DAL.Models.DbModels.Profile> uowProfile)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowProfile = uowProfile;
        }
        #endregion

        #region GET
        public async Task<List<GetProfilesByProfileType>> GetProfilesByProfileTypes(string shortName)
        {
            var results = new List<GetProfilesByProfileType>();

            // Connection string - adjust as necessary
            var connectionString = _uowProfile.GetDbContext().Database.GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("dbo.spGetProfilesByProfileType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter for the stored procedure
                    command.Parameters.Add(new SqlParameter("@shortname", SqlDbType.NVarChar)
                    {
                        Value = string.IsNullOrEmpty(shortName) ? DBNull.Value : shortName
                    });

                    await connection.OpenAsync();

                    // Execute the command and read the data
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Map each row to an instance of GetProfilesByProfileType
                            var profile = new GetProfilesByProfileType
                            {
                                ProfileTypeId = reader.GetGuid(reader.GetOrdinal("ProfileTypeId")),
                                ProfileTypeName = reader.GetString(reader.GetOrdinal("ProfileTypeName")),
                                ProfileTypeShortName = reader.GetString(reader.GetOrdinal("ProfileTypeShortName")),
                                ParentProfileTypeId = reader.IsDBNull(reader.GetOrdinal("ParentProfileTypeId"))?(Guid?)null
            : reader.GetGuid(reader.GetOrdinal("ParentProfileTypeId")),
                                ProfileName = reader.GetString(reader.GetOrdinal("ProfileName")),
                                ProfileShortName = reader.GetString(reader.GetOrdinal("ProfileShortName")),
                                ProfileId = reader.GetGuid(reader.GetOrdinal("ProfileId")),
                                ParentProfileId = reader.IsDBNull(reader.GetOrdinal("ParentProfileId"))
            ? (Guid?)null
            : reader.GetGuid(reader.GetOrdinal("ParentProfileId")),
                                CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"))
                            };

                            results.Add(profile);
                        }
                    }
                }
            }

            return results;
        }
        #endregion

    }
}
