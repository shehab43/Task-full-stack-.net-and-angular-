using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : BaseModel
    {
        private readonly Context _context;
        protected DbSet<T> _dbSet;

        public GeneralRepository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();

        }



        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            return entity;

        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.Where(c => !c.IsDeleted);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }





        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet
               .Where(c => c.Id == id && !c.IsDeleted)
               .FirstOrDefaultAsync();
        }


        public bool Update(int id, T entity)
        {
            var existingRecord = _dbSet.Where(c => c.Id == id).FirstOrDefault();

            if (existingRecord != null)
            {
                _dbSet.Update(entity);
                return true;
            }

            return false;
        }

        //Update Include
        public void UpdateInclude(T entity, params string[] modifiedProperties)
        {
            if (!_dbSet.Any(x => x.Id == entity.Id && !x.IsDeleted))
                return;

            var local = _dbSet.Local.FirstOrDefault(x => x.Id == entity.Id);
            EntityEntry entityEntry;

            if (local is null)
                entityEntry = _context.Entry(entity);
            else
                entityEntry = _context.ChangeTracker.Entries<T>().FirstOrDefault(x => x.Entity.Id == entity.Id);


            foreach (var prop in entityEntry.Properties)
            {
                if (modifiedProperties.Contains(prop.Metadata.Name))
                {
                    prop.CurrentValue = entity.GetType().GetProperty(prop.Metadata.Name).GetValue(entity);
                    prop.IsModified = true;
                }
            }

        }


        public async Task<T> GetByIdWithTracking(int id)
        {
            return await _dbSet
                .Where(c => !c.IsDeleted && c.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Delete(int id)
        {

            var entity = await GetByIdWithTracking(id);
            entity.IsDeleted = true;
            return true;

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            var count = await _context.SaveChangesAsync();
            return count;
        }


    }
}
