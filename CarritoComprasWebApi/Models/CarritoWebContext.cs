using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarritoComprasWebApi.Models;

public partial class CarritoWebContext : DbContext
{
    public CarritoWebContext()
    {
    }

    public CarritoWebContext(DbContextOptions<CarritoWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarritoInfo> CarritoInfos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; database=CarritoWeb; Integrated Security=true; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarritoInfo>(entity =>
        {
            entity.ToTable("CarritoInfo");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Fecha)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Monto).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.MontoTotal).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCarrito).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("numeric(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
