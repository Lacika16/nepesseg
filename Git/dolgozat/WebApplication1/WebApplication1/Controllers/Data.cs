using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplication1
{
    public class AlkalmazasDbContext : DbContext
    {
        public AlkalmazasDbContext(DbContextOptions<AlkalmazasDbContext> options)
        : base(options)
        { }

        public DbSet<DbContext> Tantargyak { get; set; } 
    }
}
