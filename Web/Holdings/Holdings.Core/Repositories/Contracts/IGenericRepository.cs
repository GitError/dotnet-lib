using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Search Operations

        /// <summary>
        /// Get all entities from db
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get query for entity
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        /// <summary>
        /// Get first or default entity by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        TEntity GetFirstOrDefault(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get single entity by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Get all entities from db without include
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        int Count();

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        #endregion Search Operations

        #region CRUD Operations

        /// <summary>
        /// Add entity to db
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Update entity in the db
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Update entity in the db
        /// </summary>
        /// <param name="t">entity</param>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Update(TEntity t, object key);

        /// <summary>
        /// Delete entity from db by primary key
        /// </summary>
        /// <param name="id"></param>
        void DeleteById(int id);

        /// <summary>
        /// Delete entity from db
        /// </summary>
        /// <param name="entityToDelete"></param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Delete an array of elements
        /// </summary>
        /// <param name="entitiesToDelete"></param>
        void DeleteRange(IEnumerable<TEntity> entities);

        #endregion CRUD Operations

        #region Asynchronous Operations

        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsyn(TEntity t, object key);

        Task<int> CountAsync();

        Task<int> DeleteAsyn(TEntity entity);

        Task<ICollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}