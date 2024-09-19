using com.btc.dataaccess.Context;
using com.btc.dataaccess.Generic.Abstract;
using com.btc.type.Base.Concrete;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.dataaccess.Generic.Concerete
{
    public class GenericRepositoryDAO<T> : IGenericRepositoryDAO<T> where T : BaseEntity
    {
        public ExampleContext _context;
        public GenericRepositoryDAO(ExampleContext context)
        {
            _context = context;
        }
        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteById(long id)
        {
            T model = await _context.Set<T>().FirstOrDefaultAsync(f => f.Id == id);
            EntityEntry<T> entry = _context.Set<T>().Remove(model);
            return entry.State == EntityState.Deleted;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(long id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<T> GetWhereList(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T GetWhereSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
