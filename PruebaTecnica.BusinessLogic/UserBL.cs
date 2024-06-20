using Microsoft.Data.SqlClient;
using PruebaTecnica.BusinessEntities;
using PruebaTecnica.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BusinessLogic
{
    
    public class UserBL
    {
        private readonly UserDAL _userDAL;

        public UserBL(UserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public async Task<string> createUser(User user)
        {
            return await _userDAL.createUser(user);
        }

        public async Task<User> Login(User user)
        {
            return await _userDAL.Login(user);
        }
    }
}
