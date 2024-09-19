using com.btc.dataaccess.Generic.Abstract;
using com.btc.process.manager.Generic.Concrete;
using com.btc.process.manager.System.Abstract;
using com.btc.type.Entity.System;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.process.manager.System.Concrete
{
    public class UserManager : Manager<User>, IUserManager
    {
        IUnitOfWork _unitOfWork;
        
        public UserManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetName(int id)
        {
            return _unitOfWork.UserDAO.GetName(id);
        }

        public User GetUserByName(string name)
        {
            return _unitOfWork.UserDAO.GetUserByName(name);
        }

      
    }
}
