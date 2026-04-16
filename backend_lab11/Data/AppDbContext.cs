using backend_lab11.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_lab11.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Publisher> Publishers => Set<Publisher>();
    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("authors");
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.ToTable("publishers");
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books");
            entity.Property(x => x.Title).HasMaxLength(250).IsRequired();

            entity.HasOne(x => x.Author)
                  .WithMany(x => x.Books)
                  .HasForeignKey(x => x.AuthorId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Publisher)
                  .WithMany(x => x.Books)
                  .HasForeignKey(x => x.PublisherId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}