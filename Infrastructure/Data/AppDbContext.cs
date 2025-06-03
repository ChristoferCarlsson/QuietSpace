using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<QuietPlace> QuietPlaces => Set<QuietPlace>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Bookmark> Bookmarks => Set<Bookmark>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<QuietPlace>().ToTable("QuietPlace");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Review>().ToTable("Review");
        modelBuilder.Entity<Bookmark>().ToTable("Bookmark");

        //Configure Review
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Place)
            .WithMany(qp => qp.Reviews)
            .HasForeignKey(r => r.PlaceId);

        //Configure Bookmark 
        modelBuilder.Entity<Bookmark>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookmarks)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<Bookmark>()
            .HasOne(b => b.Place)
            .WithMany(qp => qp.Bookmarks)
            .HasForeignKey(b => b.PlaceId);

        modelBuilder.Entity<Bookmark>()
    .HasIndex(b => new { b.UserId, b.PlaceId })
    .IsUnique();
    }
}
