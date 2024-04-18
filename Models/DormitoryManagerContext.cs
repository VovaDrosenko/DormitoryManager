﻿using System;
using System.Collections.Generic;
using DormitoryManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManager.Models;

public partial class DormitoryManagerContext : DbContext
{
    public DormitoryManagerContext()
    {
    }

    public DormitoryManagerContext(DbContextOptions<DormitoryManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dormitory> Dormitories { get; set; }

    public virtual DbSet<DormitoryComendant> DormitoryComendants { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FacultyWorker> FacultyWorkers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentRoom> StudentRooms { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlServer("Server=tcp:dormitoryserver.database.windows.net,1433;Initial Catalog=DormitoryManager;Persist Security Info=False;User ID=Oleksandr;Password=Alexvoron17;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dormitory>(entity =>
        {
            entity.HasKey(e => e.DormId).HasName("XPKDormitory");

            entity.ToTable("Dormitory");

            entity.Property(e => e.DormId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("dorm_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.DormNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("dorm_number");
            entity.Property(e => e.Floors).HasColumnName("floors");

            entity.HasMany(d => d.Faculties).WithMany(p => p.Dorms)
                .UsingEntity<Dictionary<string, object>>(
                    "DormitoryFaculty",
                    r => r.HasOne<Faculty>().WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("R_25"),
                    l => l.HasOne<Dormitory>().WithMany()
                        .HasForeignKey("DormId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("R_24"),
                    j =>
                    {
                        j.HasKey("DormId", "FacultyId").HasName("XPKDormitoryFaculty");
                        j.ToTable("DormitoryFaculty");
                        j.IndexerProperty<string>("DormId")
                            .HasMaxLength(18)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("dorm_id");
                        j.IndexerProperty<string>("FacultyId")
                            .HasMaxLength(18)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("faculty_id");
                    });
        });

        modelBuilder.Entity<DormitoryComendant>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("XPKDormitoryComendant");

            entity.ToTable("DormitoryComendant");

            entity.Property(e => e.WorkerId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("worker_id");
            entity.Property(e => e.DormId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("dorm_id");

            entity.HasOne(d => d.Dorm).WithMany(p => p.DormitoryComendants)
                .HasForeignKey(d => d.DormId)
                .HasConstraintName("R_13");

            entity.HasOne(d => d.Worker).WithOne(p => p.DormitoryComendant)
                .HasForeignKey<DormitoryComendant>(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_10");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("XPKFaculty");

            entity.ToTable("Faculty");

            entity.Property(e => e.FacultyId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("faculty_id");
            entity.Property(e => e.FacultyAddress)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("faculty_address");
            entity.Property(e => e.FacultyName)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("faculty_name");
        });

        modelBuilder.Entity<FacultyWorker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("XPKFacultyWorker");

            entity.ToTable("FacultyWorker");

            entity.Property(e => e.WorkerId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("worker_id");
            entity.Property(e => e.FacultyId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("faculty_id");

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultyWorkers)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("R_14");

            entity.HasOne(d => d.Worker).WithOne(p => p.FacultyWorker)
                .HasForeignKey<FacultyWorker>(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_11");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("XPKRoles");

            entity.Property(e => e.RoleId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => new { e.RoomId, e.DormId }).HasName("XPKRoom");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("room_id");
            entity.Property(e => e.DormId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("dorm_id");
            entity.Property(e => e.NumberOfBeds)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("number_of_beds");
            entity.Property(e => e.NumberOfRoom).HasColumnName("number_of_room");
            entity.Property(e => e.ResidentsGender)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("residents_gender");

            entity.HasOne(d => d.Dorm).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.DormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_2");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("XPKStudent");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("student_id");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.FacultyId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("faculty_id");
            entity.Property(e => e.Gender)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.StudentEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("student_email");
            entity.Property(e => e.StudentLastname)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("student_lastname");
            entity.Property(e => e.StudentMiddlename)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("student_middlename");
            entity.Property(e => e.StudentName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("student_name");
            entity.Property(e => e.StudentPhone)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("student_phone");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Students)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("R_29");
        });

        modelBuilder.Entity<StudentRoom>(entity =>
        {
            entity.HasKey(e => new { e.RoomId, e.DormId, e.StudentId }).HasName("XPKStudentRoom");

            entity.ToTable("StudentRoom");

            entity.Property(e => e.RoomId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("room_id");
            entity.Property(e => e.DormId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("dorm_id");
            entity.Property(e => e.StudentId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("student_id");
            entity.Property(e => e.DateBegin)
                .HasColumnType("datetime")
                .HasColumnName("date_begin");
            entity.Property(e => e.DateEnd)
                .HasColumnType("datetime")
                .HasColumnName("date_end");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentRooms)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_28");

            entity.HasOne(d => d.Room).WithMany(p => p.StudentRooms)
                .HasForeignKey(d => new { d.RoomId, d.DormId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_27");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("XPKWorker");

            entity.ToTable("Worker");

            entity.Property(e => e.WorkerId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("worker_id");
            entity.Property(e => e.RoleId)
                .HasMaxLength(18)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("role_id");
            entity.Property(e => e.WorkerEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("worker_email");
            entity.Property(e => e.WorkerLastname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("worker_lastname");
            entity.Property(e => e.WorkerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("worker_name");
            entity.Property(e => e.WorkerPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("worker_password");
            entity.Property(e => e.WorkerPhone)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("worker_phone");
            entity.Property(e => e.WorkerSurname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("worker_surname");

            entity.HasOne(d => d.Role).WithMany(p => p.Workers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("R_15");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
