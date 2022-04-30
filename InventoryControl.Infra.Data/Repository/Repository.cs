using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public TEntity Delete(TEntity domain)
        {
            _applicationDbContext.Remove(domain);
            _applicationDbContext.SaveChanges();
            return domain;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity? FindById(int? id)
        {
            return _applicationDbContext.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll()
        {
            return _applicationDbContext.Set<TEntity>().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public TEntity Insert(TEntity domain)
        {
            _applicationDbContext.Add(domain);
            _applicationDbContext.SaveChanges();
            return domain;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public TEntity Update(TEntity domain)
        {
            _applicationDbContext.Update(domain);
            _applicationDbContext.SaveChanges();
            return domain;
        }
    }
}
