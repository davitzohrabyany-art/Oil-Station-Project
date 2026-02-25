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
    public DbSet<User_Car> User_Car { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql(
            "Server=127.0.0.1;Port=3306;Database=oilstationdb;User=root;Password=D096055655d;",
            ServerVersion.AutoDetect("Server=127.0.0.1;Port=3306;Database=oilstationdb;User=root;Password=D096055655d;")
        );
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User_Car>()
            .HasKey(uc => new { uc.UserId, uc.CarId });
    }
    
}