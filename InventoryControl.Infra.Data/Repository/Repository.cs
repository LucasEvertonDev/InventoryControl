﻿using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<TEntity> Delete(TEntity domain)
        {
            _applicationDbContext.Remove(domain);
            await _applicationDbContext.SaveChangesAsync();
            return domain;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity>? FindById(int? id)
        {
            TEntity? val = await _applicationDbContext.Set<TEntity>().FindAsync(id);
            return val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAll()
        {
            return await _applicationDbContext.Set<TEntity>().ToListAsync();
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<TEntity> Insert(TEntity domain)
        {
            _applicationDbContext.Add(domain);
            await _applicationDbContext.SaveChangesAsync();
            return domain;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<TEntity> Update(TEntity domain)
        {
            _applicationDbContext.Update(domain);
            await _applicationDbContext.SaveChangesAsync();
            return domain;
        }
    }
}
