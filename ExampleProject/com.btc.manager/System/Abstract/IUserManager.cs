using com.btc.dataaccess.Generic.Abstract;
using com.btc.process.manager.Generic.Abstract;
using com.btc.type.Entity.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.process.manager.System.Abstract
{
    public interface IUserManager : IManager<User>
    {
        string GetName(int id);
        User GetUserByName(string name);
    }
}
