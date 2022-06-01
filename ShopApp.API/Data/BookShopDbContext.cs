using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopApp.API.Models;

namespace ShopApp.API.Data
{
    public partial class BookShopDbContext : IdentityDbContext<ApiUser>
    {
        public BookShopDbContext()
        {
        }

        public BookShopDbContext(DbContextOptions<BookShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            dynamic user1;
            dynamic user2;
            dynamic role1;
            dynamic role2;

            modelBuilder.Entity<IdentityRole>().HasData(
                role1 = new IdentityRole
                {
                    Name =  "User",
                    NormalizedName = "USER",
                    Id = Guid.NewGuid().ToString(),
                },
                role2 = new IdentityRole
                {
                    Name =  "Admin",
                    NormalizedName = "ADMIN",
                    Id = Guid.NewGuid().ToString(),
                }
            );

            var hash = new PasswordHasher<ApiUser>();

            modelBuilder.Entity<ApiUser>().HasData(
                user1 = new ApiUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "tenas@gmail.com",
                    NormalizedEmail = "TENAS@GMAIL.COM",
                    FirstName = "Tenas",
                    LastName = "Steve",
                    UserName = "stevetenas",
                    NormalizedUserName = "STEVETENAS",
                    PasswordHash = hash.HashPassword(null, "P@ssword1")
                },
                user2 = new ApiUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user@gmail.com",
                    NormalizedEmail = "USER@GMAIL.COM",
                    FirstName = "System",
                    LastName = "User",
                    UserName = "systemuser",
                    NormalizedUserName = "SYSTEMUSER",
                    PasswordHash = hash.HashPassword(null, "P@ssword1")
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = role1.Id,
                    UserId = user1.Id,
                },
                new IdentityUserRole<string>
                {
                    RoleId = role2.Id,
                    UserId = user2.Id,
                }
            );

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA00743D88")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_Authors_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
