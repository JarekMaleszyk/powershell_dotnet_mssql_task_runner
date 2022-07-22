using Microsoft.EntityFrameworkCore;
using offler_db_context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offler_db_context.Context
{
    public class OfflerDbContext : DbContext
    {
        public DbSet<TalendConfig> TalendConfig { get; set; }
        public OfflerDbContext(DbContextOptions<OfflerDbContext> options) : base (options)
        {

        }
    }
}
