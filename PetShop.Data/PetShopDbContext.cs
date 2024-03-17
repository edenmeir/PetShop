using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PetShop.Models.Entities;

namespace PetShop.Data;

public partial class PetShopDbContext : DbContext
{
    public PetShopDbContext()
    {
    }

    public PetShopDbContext(DbContextOptions<PetShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=EDENS-PC\\EDENS_SERVER;Initial Catalog=PetShop2;Integrated Security=True;trustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.Animals).HasConstraintName("FK_Animals_Categories");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name).IsFixedLength();
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.Animal).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Animals");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasOne(d => d.Animal).WithMany(p => p.Pictures)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_Animals");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
