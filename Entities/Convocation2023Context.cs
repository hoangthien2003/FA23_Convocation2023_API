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

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=db.fjourney.site;Initial Catalog=Convocation2023;User ID=sa;Password=<YourStrong@Passw0rda>;TrustServerCertificate=true;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Vietnamese_CI_AS");

        modelBuilder.Entity<Bachelor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bachelor__3214EC07BBA70F7D");

            entity.ToTable("Bachelor");

            entity.Property(e => e.Chair)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChairParent)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Faculty)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.HallName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Image)
                .HasMaxLength(250)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Major)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");
            entity.Property(e => e.StatusBaChelor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.TimeCheckIn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.ToTable("CheckIn");

            entity.Property(e => e.CheckinId).HasColumnName("CheckinID");
            entity.Property(e => e.HallName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.HasKey(e => e.HallId).HasName("PK__Hall__7E60E2149F83D79B");

            entity.ToTable("Hall");

            entity.Property(e => e.HallId).ValueGeneratedNever();
            entity.Property(e => e.HallName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Session__C9F492901585092D");

            entity.ToTable("Session");

            entity.Property(e => e.SessionId).ValueGeneratedNever();
            entity.Property(e => e.Session1).HasColumnName("Session");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.RoleId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
