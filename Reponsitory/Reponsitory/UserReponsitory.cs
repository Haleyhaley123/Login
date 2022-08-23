using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using Reponsitory.Interface;
using Scrypt;
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
        public async Task<bool> LoginAccount(string username, string password)
        {
            try
            {
                var checkaccount = await _dataBaseContext.RegisterAccounts.Where(e => e.UserName == username).FirstOrDefaultAsync<RegisterAccount>();
                if (checkaccount != null)
                {
                    ScryptEncoder encoder = new ScryptEncoder();
                    var pass = encoder.Compare(password, checkaccount.PasswordHash);
                    if (pass)
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
                var checkregistation = await _dataBaseContext.RegisterAccounts.FirstOrDefaultAsync(e => e.UserName == request.UserName || e.Email == request.Email);
                if (checkregistation != null )
                {
                  return false;                    
                }
                ScryptEncoder encoder = new ScryptEncoder();
                RegisterAccount resg = new RegisterAccount
                {
                    UserId = Guid.NewGuid(),
                    UserName = request.UserName,
                    PasswordHash = encoder.Encode(request.PasswordHash),
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
