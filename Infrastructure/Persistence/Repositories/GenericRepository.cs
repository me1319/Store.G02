using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async  Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return trackChanges ?
             await _context.Products.Include(P => P.ProductType).Include(P => P.ProductBrand).ToListAsync() as IEnumerable<TEntity>
             : await _context.Products.Include(P => P.ProductType).Include(P => P.ProductBrand).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }

            return trackChanges ?
                await _context.Set<TEntity>().ToListAsync()
                :await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            //if(tarckChanges) return await _context.Set<TEntity>().ToListAsync();
            //return await _context. Set<TEntity>() . AsNoTracking() . ToListAsync();
        }
       public async Task<TEntity> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.ProductType).Include(P => P.ProductBrand).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }
        async Task IGenericRepository<TEntity, TKey>.AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
        void IGenericRepository<TEntity, TKey>.Update(TEntity entity)
        {
            _context.Update(entity);
        }

        void IGenericRepository<TEntity, TKey>.Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
    }
}
