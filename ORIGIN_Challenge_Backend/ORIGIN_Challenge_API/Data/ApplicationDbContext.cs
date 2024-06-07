using Microsoft.EntityFrameworkCore;
using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tarjeta> Tarjetas { get; set; }
        public virtual DbSet<Operaciones> Operaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.ToTable("Tarjetas");

                entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta").UseIdentityColumn();
                entity.Property(e => e.NumeroTarjeta).HasColumnName("numero_tarjeta").IsRequired().HasMaxLength(16);
                entity.Property(e => e.Pin).HasColumnName("pin").IsRequired().HasMaxLength(4);
                entity.Property(e => e.Bloqueada).HasColumnName("bloqueada").HasDefaultValue(false);
                entity.Property(e => e.DineroEnCuenta).HasColumnName("dinero_en_cuenta").IsRequired().HasColumnType("decimal(10, 2)");
                entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento").IsRequired();

            });

            modelBuilder.Entity<Operaciones>(entity =>
            {
                entity.ToTable("Operaciones");

                entity.Property(e => e.IdOperacion).HasColumnName("id_operacion").UseIdentityColumn();
                entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta").IsRequired();
                entity.Property(e => e.Fecha).HasColumnName("fecha").IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.CodigoOperacion).HasColumnName("codigo_operacion").IsRequired();
                entity.Property(e => e.CantidadRetiro).HasColumnName("cantidad_retiro").HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Balance).HasColumnName("balance").HasColumnType("decimal(10, 2)");
            });
        }
    }
}

