using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitory.Interface
{
    public interface IUserReponsitory
    {
        Task<UserModel> LoginAccount(string username, string password);
        Task<bool> Registation (UserModel request);
    }
}
