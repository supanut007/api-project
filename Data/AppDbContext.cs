using Microsoft.EntityFrameworkCore;
using ProjectApi.Models; // ใช้ namespace ของ Model

namespace ProjectApi.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor รับ options จาก DI
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet คือ Table
        public DbSet<Product> Products { get; set; }

        // (ถ้าอยาก Config เพิ่ม)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ตัวอย่าง: ตั้งชื่อ Table
            modelBuilder.Entity<Product>().ToTable("products");
        }
    }
}
