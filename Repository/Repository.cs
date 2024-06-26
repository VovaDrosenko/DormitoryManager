﻿using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.Context;
using DormitoryManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManager.Repository
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        internal AppDbContext context;
        internal DbSet<TEntity> dbSet;
        public Repository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task Delete(object id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
            {
                await Delete(entityToDelete);
            }
        }

        public async Task Delete(TEntity entityToDelete)
        {
            await Task.Run(
                () =>
                {
                    if (context.Entry(entityToDelete).State == EntityState.Detached)
                    {
                        dbSet.Attach(entityToDelete);
                    }
                    dbSet.Remove(entityToDelete);
                });
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(dbSet, specification);
        }

        public async Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(TEntity ententityToUpdate)
        {
            var existingEntity = await context.Set<TEntity>().FindAsync(ententityToUpdate.Id);
            if (existingEntity != null) {
                context.Entry(existingEntity).CurrentValues.SetValues(ententityToUpdate);
                await context.SaveChangesAsync();
            }
            //await Task.Run
            //    (
            //    () =>
            //    {
            //        dbSet.Attach(ententityToUpdate);
            //        context.Entry(ententityToUpdate).State = EntityState.Modified;
            //    });
        }
    }
}
