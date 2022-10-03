using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DBContext
{
    public class DBContext
    {
        public class DatabaseContext : DbContext
        {
            public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
            {

            }           
            public DbSet<UserModel> Users { get; set; }
            //public DbSet<UploadImage> UpLoadImages { get; set; }
            public DbSet<RolesModel> Roles { get; set; }


        }
    }
}
