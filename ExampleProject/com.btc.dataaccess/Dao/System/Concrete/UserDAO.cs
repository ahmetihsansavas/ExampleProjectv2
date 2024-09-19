using com.btc.dataaccess.Context;
using com.btc.dataaccess.Dao.System.Abstract;
using com.btc.dataaccess.Generic.Abstract;
using com.btc.dataaccess.Generic.Concerete;
using com.btc.type.Entity.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.dataaccess.Dao.System.Concrete
{
    public class UserDAO : GenericRepositoryDAO<User>, IUserDAO
    {
        ExampleContext _context;
        public UserDAO(ExampleContext context) : base(context)
        {
            _context = context;
        }

        public string GetName(int id)
        {
            var user = _context.Set<User>().Where(u => u.Id == id).FirstOrDefault();
            if (user!=null)
            {
                return user.Name;
            }
            return "";
        }

        public User GetUserByName(string name)
        {
            var user = _context.Set<User>().Where(u => u.UserCode == name).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
