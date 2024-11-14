﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FA23_Convocation2023_API.Models
{
    public partial class Convo24Context : DbContext
    {
        public Convo24Context()
        {
        }

        public Convo24Context(DbContextOptions<Convo24Context> options)
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
            modelBuilder.Entity<Bachelor>(entity =>
            {
                entity.ToTable("Bachelor");

                entity.HasIndex(e => e.HallId, "IX_Bachelor_HallId");

                entity.HasIndex(e => e.SessionId, "IX_Bachelor_SessionId");

                entity.Property(e => e.Chair)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChairParent)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Faculty)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

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

                entity.Property(e => e.StatusBaChelor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TimeCheckIn).HasColumnType("datetime");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Bachelors)
                    .HasForeignKey(d => d.HallId)
                    .HasConstraintName("FK__Bachelor__HallId__403A8C7D");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Bachelors)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK__Bachelor__Sessio__412EB0B6");
            });

            modelBuilder.Entity<CheckIn>(entity =>
            {
                entity.ToTable("CheckIn");

                entity.HasIndex(e => e.HallId, "IX_CheckIn_HallId");

                entity.HasIndex(e => e.SessionId, "IX_CheckIn_SessionId");

                entity.Property(e => e.CheckinId).HasColumnName("CheckinID");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.CheckIns)
                    .HasForeignKey(d => d.HallId)
                    .HasConstraintName("FK__CheckIn__HallId__440B1D61");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.CheckIns)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK__CheckIn__Session__44FF419A");
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.ToTable("Hall");

                entity.Property(e => e.HallName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
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

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.Session1).HasColumnName("Session");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_Users_RoleID");

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

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleID__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}