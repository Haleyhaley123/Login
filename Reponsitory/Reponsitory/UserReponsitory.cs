using Azure.Core;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using Reponsitory.Interface;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<UserModel> LoginAccount(string username, string password)
        {
            try
            {
                UserModel data = new UserModel();
                var checkaccount = await _dataBaseContext.Users.FirstOrDefaultAsync(e => e.UserName == username);
                if (checkaccount != null)
                {
                    ScryptEncoder encoder = new ScryptEncoder();
                    var pass = encoder.Compare(password, checkaccount.PasswordHash);
                    if (pass)
                    {
                        return data = checkaccount;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null; 
            }
        }

        public async Task<bool> Registation(UserModel request)
        {
            
            try
            {
                var checkregistation = await _dataBaseContext.Users.FirstOrDefaultAsync(e => e.UserName == request.UserName || e.Email == request.Email);
                if (checkregistation != null )
                {
                  return false;                    
                }
                ScryptEncoder encoder = new ScryptEncoder();
                UserModel resg = new UserModel
                {
                    UserId = Guid.NewGuid(),
                    UserName = request.UserName,
                    PasswordHash = encoder.Encode(request.PasswordHash),
                    Email = request.Email,
                    Phone = request.Phone,
                    Fullname = request.Fullname,
                    Status = false,
                    RoleCode = request.RoleCode
                };
                try
                {
                    _dataBaseContext.Users.Add(resg);
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
        private ClaimsIdentity CreateIdentity(UserModel user)
        {
            string username = string.IsNullOrEmpty(user.UserName) ? "" : user.UserName;
            //string roles = string.IsNullOrEmpty(user.RoleId) ? "" : user.RoleId;
           var rolecode =  _dataBaseContext.Roles.FirstOrDefaultAsync(e => e.RoleCode == user.RoleCode);
            string role = "";
            role = rolecode.ToString();
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("RoleCode", role)                
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            return identity;
        }
    }
}
