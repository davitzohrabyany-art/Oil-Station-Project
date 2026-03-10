using Microsoft.EntityFrameworkCore;
using OilChangeApp.Domain.Entities;

namespace OilChangeApp;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Oil_change> Oil_change { get; set; }
    public DbSet<Service_visit> Service_visit { get; set; }
    public DbSet<Car> Car { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<user_Car> User_Car { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql(
            DbConnectionFactory.connectionString ,
            ServerVersion.AutoDetect(DbConnectionFactory.connectionString)
        );
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<user_Car>()
            .HasKey(uc => new { uc.UserId, uc.CarId });

        modelBuilder.Entity<user_Car>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserCars)
            .HasForeignKey(uc => uc.UserId);
        
        modelBuilder.Entity<user_Car>()
            .HasOne(uc => uc.Car)
            .WithMany(c => c.UserCars)
            .HasForeignKey(uc => uc.CarId);
    }
    
}