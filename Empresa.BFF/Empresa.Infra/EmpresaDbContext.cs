using Empresa.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Empresa.Infra
{
    public class EmpresaDbContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Client> Client { get; set; }
        public DbSet<Lead> Leads { get; set; }

        public EmpresaDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("Empresa");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lead>()
                .HasOne(l => l.Client)
                .WithMany()
                .HasForeignKey(l => l.IdClient);
        }
    }
}
