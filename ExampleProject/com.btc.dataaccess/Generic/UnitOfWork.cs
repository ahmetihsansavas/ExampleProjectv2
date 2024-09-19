using com.btc.dataaccess.Context;
using com.btc.dataaccess.Dao.System.Abstract;
using com.btc.dataaccess.Dao.System.Concrete;
using com.btc.dataaccess.Generic.Abstract;
using com.btc.dataaccess.Generic.Concerete;
using com.btc.type.Base.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.dataaccess.Generic.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExampleContext _dbContext;

        public UnitOfWork(ExampleContext dbContext)
        {
            _dbContext = dbContext;

        }

        private bool disposed = false;

        private IUserDAO _UserDAO;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                // Transaction işlemleri burada ele alınabilir veya Identity Map kurumsal tasarım kalıbı kullanılarak
                // sadece değişen alanları güncellemeyide sağlayabiliriz.
               return await _dbContext.SaveChangesAsync();
            }
            catch
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.
                throw;

            }
        }

        public IGenericRepositoryDAO<T> GetRepository<T>() where T : BaseEntity
        {
            return new GenericRepositoryDAO<T>(_dbContext);
        }

        public IUserDAO UserDAO
        {
            get
            {
                return _UserDAO ?? (_UserDAO = new UserDAO(_dbContext));
            }
        }
    }
}
