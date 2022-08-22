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
            public DbSet<RegisterAccount> RegisterAccounts { get; set; }
        }
    }
}
