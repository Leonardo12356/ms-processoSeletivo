using Microsoft.EntityFrameworkCore;
using ms_processoSeletivo.Models;

namespace ms_processoSeletivo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {

        }

        public virtual DbSet<Pessoa> Pessoas { get; set; }
    }
}