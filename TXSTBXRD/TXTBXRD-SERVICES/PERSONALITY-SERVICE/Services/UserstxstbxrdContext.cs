using PERSONALITY_SERVICE.Models;
using Microsoft.EntityFrameworkCore;

namespace PERSONALITY_SERVICE.Services;

public partial class UserstxstbxrdContext : DbContext
{
    public UserstxstbxrdContext() { }

    public UserstxstbxrdContext(DbContextOptions<UserstxstbxrdContext> options)
        : base(options) { }

    public virtual DbSet<Detail>? Details { get; set; }

    public virtual DbSet<Permission>? Permissions { get; set; }

    public virtual DbSet<Salt>? Salts { get; set; }

    public virtual DbSet<User>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("details");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("lastName");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("userName");

            entity.HasOne(d => d.User).WithMany(p => p.Details)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("details_ibfk_1");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Salt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("salt");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");

            entity.HasOne(d => d.User).WithMany(p => p.Salts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salt_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("users", tb => tb.HasComment("Table \"Users\""))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Enabled);

            entity.HasMany(d => d.Permissions).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersPermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("Permission")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("users_permissions_ibfk_2"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("users_permissions_ibfk_1"),
                    j =>
                    {
                        j.HasKey("User", "Permission")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("users_permissions");
                        j.HasIndex(new[] { "Permission" }, "permission");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
