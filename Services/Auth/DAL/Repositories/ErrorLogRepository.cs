using DAL.Models.DbModels;
using DAL.Repositories.UOW;
//
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthDAL.Repositories
{
    public class ErrorLogRepository<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly AdvancedLearningSystemDbContext _dbContext;


        #endregion

        #region Constructor

        public ErrorLogRepository(AdvancedLearningSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region CUD Operations

        public async Task<ErrorLog> Create(ErrorLog input)
        {
            using var _uowErrorLog = new UnitOfWork<ErrorLog>();
            var res = _uowErrorLog.Repository.Insert(input);
            await _uowErrorLog.Save();

            return await Task.FromResult(input);
        }

        public async Task<ErrorLog> Update(ErrorLog input)
        {
            using var _uowErrorLog = new UnitOfWork<ErrorLog>();
            _uowErrorLog.Repository.Update(input);
            await _uowErrorLog.Save();

            return await Task.FromResult(input);
        }

        public async Task<bool> Delete(ErrorLog input)
        {
            using var _uowErrorLog = new UnitOfWork<ErrorLog>();
            _uowErrorLog.Repository.Update(input);
            await _uowErrorLog.Save();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<ErrorLog>> GetALL(Expression<Func<ErrorLog, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            using var _uowErrorLog = new UnitOfWork<ErrorLog>();
            return await _uowErrorLog.Repository.GetALL(filter).ToListAsync();
        }

        public async Task<ErrorLog?> GetById(object Id)
        {
            using var _uowErrorLog = new UnitOfWork<ErrorLog>();
            var obj = await _uowErrorLog.Repository.GetById(Id);
            return obj;
        }


        #endregion

        #region Helper Methods




        #endregion
    }
}
