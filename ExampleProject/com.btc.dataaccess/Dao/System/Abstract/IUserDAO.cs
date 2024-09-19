using com.btc.dataaccess.Generic.Abstract;
using com.btc.type.Entity.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.dataaccess.Dao.System.Abstract
{
    public interface IUserDAO:IGenericRepositoryDAO<User>
    {
        string GetName(int id);
        User GetUserByName(string name);
    }
}
