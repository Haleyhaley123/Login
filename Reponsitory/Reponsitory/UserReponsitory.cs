using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.DBContext.DBContext;

namespace Reponsitory.Reponsitory
{
    public class UserReponsitory : IUserReponsitory
    {
        private readonly DatabaseContext _dataBaseContext;
        public UserReponsitory(DatabaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public async Task<bool> LoginAccount(LoginModel request)
        {
            try
            {
                var checkaccount = await _dataBaseContext.RegisterAccounts.FirstOrDefaultAsync(e => e.UserName == request.UserName);
                if (checkaccount != null)
                {
                    bool isverifypassword = BCrypt.Net.BCrypt.Verify(checkaccount.PasswordHash, request.Password);
                    if (isverifypassword)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Registation(RegisterAccount request)
        {
            
            try
            {
                var checkregistation = await _dataBaseContext.RegisterAccounts.FirstOrDefaultAsync(e => e.UserName == request.UserName || e.Email == request.Email || e.Phone == request.Phone);
                if (checkregistation != null )
                {
                  return false;                    
                }
                RegisterAccount resg = new RegisterAccount
                {
                  
                    UserName = request.UserName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash),
                    Email = request.Email,
                    Phone = request.Phone,
                    Fullname = request.Fullname,
                    Status = false
                };
                try
                {
                    _dataBaseContext.RegisterAccounts.Add(resg);
                    await _dataBaseContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }               

        }
    }
}
