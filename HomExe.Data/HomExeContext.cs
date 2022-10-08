using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomExe.Data
{
    public partial class HomExeContext : DbContext
    {
        public HomExeContext()
        {
        }

        public HomExeContext(DbContextOptions<HomExeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<Pt> Pts { get; set; } = null!;
        public virtual DbSet<PtCategory> PtCategories { get; set; } = null!;
        public virtual DbSet<Recipee> Recipees { get; set; } = null!;
        public virtual DbSet<RecipeeCategory> RecipeeCategories { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-GFSC70P;Initial Catalog=HomExe;uid=sa;pwd=1;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("Contract");

                entity.Property(e => e.ContractId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(50)
                    .HasColumnName("Created_date");

                entity.Property(e => e.EndDate)
                    .HasMaxLength(50)
                    .HasColumnName("End_date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.ContractNavigation)
                    .WithOne(p => p.Contract)
                    .HasForeignKey<Contract>(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_User1");

                entity.HasOne(d => d.Pt)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.PtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_PT");
            });

            modelBuilder.Entity<Pt>(entity =>
            {
                entity.ToTable("PT");

                entity.Property(e => e.PtId).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LinkMeet).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Pts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PT_PtCategory");
            });

            modelBuilder.Entity<PtCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("PtCategory");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.Category).HasMaxLength(50);
            });

            modelBuilder.Entity<Recipee>(entity =>
            {
                entity.HasKey(e => e.RecipeId);

                entity.ToTable("Recipee");

                entity.Property(e => e.RecipeId).ValueGeneratedNever();

                entity.Property(e => e.Pictures).HasMaxLength(50);

                entity.Property(e => e.Recipe).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Recipees)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipee_RecipeeCategory");
            });

            modelBuilder.Entity<RecipeeCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("RecipeeCategory");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.Category).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.Role1)
                    .HasMaxLength(50)
                    .HasColumnName("Role");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.ScheduleId).ValueGeneratedNever();

                entity.Property(e => e.Date).HasMaxLength(50);

                entity.HasOne(d => d.ScheduleNavigation)
                    .WithOne(p => p.Schedule)
                    .HasForeignKey<Schedule>(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_PT1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Height).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.Weight).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
