using com.btc.type.Base.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.dataaccess.Generic.Abstract
{
    public interface IGenericRepositoryDAO<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetById(long id);
        Task Create(T entity);
        Task Update(T entity);
        Task<bool> DeleteById(long id);
        IQueryable<T> GetWhereList(Expression<Func<T, bool>> predicate);
        T GetWhereSingle(Expression<Func<T, bool>> predicate);
    }
}
