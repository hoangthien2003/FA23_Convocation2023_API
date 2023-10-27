using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FA23_Convocation2023_API.Entities;

public partial class Convocation2023Context : DbContext
{
    public Convocation2023Context()
    {
    }

    public Convocation2023Context(DbContextOptions<Convocation2023Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Bachelor> Bachelors { get; set; }

    public virtual DbSet<CheckIn> CheckIns { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=Convocation2023;Integrated Security=True;Trusted_Connection=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bachelor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bachelor__3214EC07249BBB6D");

            entity.ToTable("Bachelor");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Faculty)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Major)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");
            entity.Property(e => e.StatusBachelor)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CheckIn__3214EC077D9ACBD1");

            entity.ToTable("CheckIn");

            entity.Property(e => e.BachelorId).HasColumnName("BachelorID");
            entity.Property(e => e.CheckIn1).HasDefaultValueSql("((0))");
            entity.Property(e => e.CheckIn2).HasDefaultValueSql("((0))");
            entity.Property(e => e.TimeCheckIn1).HasColumnType("datetime");
            entity.Property(e => e.TimeCheckIn2).HasColumnType("datetime");

            entity.HasOne(d => d.Bachelor).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.BachelorId)
                .HasConstraintName("FK__CheckIn__Bachelo__3B75D760");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
