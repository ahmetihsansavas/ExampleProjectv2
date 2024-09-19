using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.process.manager.Generic.Abstract
{
    public interface IManager<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetById(long id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> DeleteById(long id);
        IQueryable<T> GetWhereList(Expression<Func<T, bool>> predicate);
        T GetWhereSingle(Expression<Func<T, bool>> predicate);
    }
}
