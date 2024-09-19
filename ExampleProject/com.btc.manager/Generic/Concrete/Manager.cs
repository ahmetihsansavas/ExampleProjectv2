using com.btc.dataaccess.Generic.Abstract;
using com.btc.process.manager.Generic.Abstract;
using com.btc.type.Base.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.process.manager.Generic.Concrete
{
    public class Manager<T> : IManager<T> where T : BaseEntity
    {
        private readonly IUnitOfWork unitOfWork;

        public Manager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<T> Create(T entity)
        {
           await unitOfWork.GetRepository<T>().Create(entity);
           await unitOfWork.SaveChanges();
           return entity;
        }

        public async Task<bool> DeleteById(long id)
        {
           return await unitOfWork.GetRepository<T>().DeleteById(id);
        }

        public IQueryable<T> GetAll()
        {
           return unitOfWork.GetRepository<T>().GetAll();
        }

        public Task<T> GetById(long id)
        {
            return unitOfWork.GetRepository<T>().GetById(id);
        }

        public IQueryable<T> GetWhereList(Expression<Func<T, bool>> predicate)
        {
            return unitOfWork.GetRepository<T>().GetWhereList(predicate);
        }

        public T GetWhereSingle(Expression<Func<T, bool>> predicate)
        {
            return unitOfWork.GetRepository<T>().GetWhereSingle(predicate);
        }

        public async Task<T> Update(T entity)
        {
           await unitOfWork.GetRepository<T>().Update(entity);
           await unitOfWork.SaveChanges();
           return entity;
        }
    }
}
