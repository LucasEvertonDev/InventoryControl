using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected ApplicationDbContext _applicationDbContext;

        public IQueryable<TEntity> Table => _applicationDbContext.Set<TEntity>().AsQueryable();

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Task<TEntity> Delete(TEntity domain)
        {
            _applicationDbContext.Remove(domain);
            return Task.FromResult(domain);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity>? FindById(int? id)
        {
            return await _applicationDbContext.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Task<TEntity> Insert(TEntity domain)
        {
            _applicationDbContext.Add(domain);
            return Task.FromResult(domain);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Task<TEntity> Update(TEntity domain)
        {
            _applicationDbContext.Update(domain);
            return Task.FromResult(domain);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
