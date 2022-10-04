using Microsoft.EntityFrameworkCore;
using ThucHanhDangNhap.Constants;
using ThucHanhDangNhap.Models;

namespace ThucHanhDangNhap.DBContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id);
            entity.HasOne(u => u.Customer);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.Property(e => e.Username)
                .IsUnicode(false)
                .IsRequired();
            entity.Property(e => e.Email)
                .IsUnicode(false)
                .IsRequired();
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsRequired();
            entity.Property(e => e.UserType)
                .HasDefaultValue(UserTypes.Customer)
                .IsRequired();
            entity.Property(e => e.UserStatus)
                .HasDefaultValue(UserStatus.UnBlocked)
                .IsRequired();
            entity.HasAlternateKey(u => u.Username);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.Property(e => e.Fullname)
                .IsRequired();
            entity.Property(e => e.DateOfBirth)
                .IsRequired();
            entity.Property(e => e.CCCD)
                .IsRequired();
            entity.Property(e => e.Address);
            entity.HasAlternateKey(e => e.CCCD);
        });
    }
}