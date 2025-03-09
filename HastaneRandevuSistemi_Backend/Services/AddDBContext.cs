using Microsoft.EntityFrameworkCore;
using randevuSistemi.Models;

namespace randevuSistemi.Services
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Hastane> Hastaneler { get; set; }
        public DbSet<Poliklinik> Poliklinikler { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        
        public DbSet<Randevu> Randevular {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Randevu>()
        .HasOne(r => r.Poliklinik)
        .WithMany()
        .HasForeignKey(r => r.PoliklinikId)
        .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict kullanılıyor

    modelBuilder.Entity<Randevu>()
        .HasOne(r => r.Hastane)
        .WithMany()
        .HasForeignKey(r => r.HastaneId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Randevu>()
        .HasOne(r => r.Doktor)
        .WithMany()
        .HasForeignKey(r => r.DoktorId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Randevu>()
        .HasOne(r => r.Kullanici)
        .WithMany()
        .HasForeignKey(r => r.KullaniciId)
        .OnDelete(DeleteBehavior.Restrict);

        
}


      
       
      
    }
}
