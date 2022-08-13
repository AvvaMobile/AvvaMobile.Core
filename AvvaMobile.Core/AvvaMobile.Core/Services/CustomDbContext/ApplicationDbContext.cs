using AvvaMobile.Core.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.CustomDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AppSettings> AppSettings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
