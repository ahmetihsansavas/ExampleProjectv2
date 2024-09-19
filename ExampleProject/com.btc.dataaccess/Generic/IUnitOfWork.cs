using com.btc.dataaccess.Dao.System.Abstract;
using com.btc.type.Base.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.dataaccess.Generic.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        public IUserDAO UserDAO { get; }
        IGenericRepositoryDAO<T> GetRepository<T>() where T : BaseEntity;
        Task<int> SaveChanges();
    }
}
