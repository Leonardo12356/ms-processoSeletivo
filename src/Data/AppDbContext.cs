using Microsoft.EntityFrameworkCore;
using ms_processoSeletivo.Models;

namespace ms_processoSeletivo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Pessoas = Set<Pessoa>();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
            Pessoas = Set<Pessoa>();
        }

        public virtual DbSet<Pessoa> Pessoas { get; set; }
    }
}