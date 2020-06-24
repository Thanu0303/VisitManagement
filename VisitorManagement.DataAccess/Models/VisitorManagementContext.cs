using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VisitorManagement.DataAccess.Models
{
    public partial class VisitorManagementContext : DbContext
    {
        public VisitorManagementContext()
        {
        }

        public VisitorManagementContext(DbContextOptions<VisitorManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Arrangement> Arrangement { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VisitArrangement> VisitArrangement { get; set; }
        public virtual DbSet<VisitDetail> VisitDetail { get; set; }
        public virtual DbSet<VisitLog> VisitLog { get; set; }
        public virtual DbSet<Visitor> Visitor { get; set; }
        public virtual DbSet<VisitParticipant> VisitParticipant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=BHIMA\\SQLDEV2016;Database=VisitorManagement;user id=sa;password=sa;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Arrangement>(entity =>
            {
                entity.Property(e => e.ArrangementId).HasColumnName("ArrangementID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress).HasMaxLength(500);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.InactivateDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PreWindows2000Username)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<VisitArrangement>(entity =>
            {
                entity.Property(e => e.VisitArrangementId).HasColumnName("VisitArrangementID");

                entity.Property(e => e.ArrangementId).HasColumnName("ArrangementID");

                entity.Property(e => e.DelegateContactId).HasColumnName("DelegateContactID");

                entity.Property(e => e.VisitDetailId).HasColumnName("VisitDetailID");

                entity.HasOne(d => d.Arrangement)
                    .WithMany(p => p.VisitArrangement)
                    .HasForeignKey(d => d.ArrangementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitArrangement_Arrangement");

                entity.HasOne(d => d.VisitDetail)
                    .WithMany(p => p.VisitArrangement)
                    .HasForeignKey(d => d.VisitDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitArrangement_VisitDetail");
            });

            modelBuilder.Entity<VisitDetail>(entity =>
            {
                entity.Property(e => e.VisitDetailId).HasColumnName("VisitDetailID");

                entity.Property(e => e.ActualTimeIn).HasColumnType("datetime");

                entity.Property(e => e.ActualTimeOut).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.VisitorId).HasColumnName("VisitorID");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.VisitDetail)
                    .HasForeignKey(d => d.VisitorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitDetail_Visitor");
            });

            modelBuilder.Entity<VisitLog>(entity =>
            {
                entity.Property(e => e.VisitLogId).HasColumnName("VisitLogID");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateUtc)
                    .HasColumnName("CreatedDateUTC")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LogXml).HasColumnName("LogXML");

                entity.Property(e => e.VisitDetailId).HasColumnName("VisitDetailID");

                entity.HasOne(d => d.VisitDetail)
                    .WithMany(p => p.VisitLog)
                    .HasForeignKey(d => d.VisitDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitLog_VisitDetail");
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.Property(e => e.VisitorId).HasColumnName("VisitorID");

                entity.Property(e => e.Company).HasMaxLength(255);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Designation).HasMaxLength(255);

                entity.Property(e => e.EmailAddress).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoUrl)
                    .IsRequired()
                    .HasColumnName("PhotoURL")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.VisitorName).HasMaxLength(510);
            });

            modelBuilder.Entity<VisitParticipant>(entity =>
            {
                entity.Property(e => e.VisitParticipantId).HasColumnName("VisitParticipantID");

                entity.Property(e => e.ParticipantId).HasColumnName("ParticipantID");

                entity.Property(e => e.VisitDetailId).HasColumnName("VisitDetailID");

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.VisitParticipant)
                    .HasForeignKey(d => d.ParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitParticipant_User");


                entity.HasOne(d => d.VisitDetail)
                    .WithMany(p => p.VisitParticipant)
                    .HasForeignKey(d => d.VisitDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitParticipant_VisitDetail");
            });
        }
    }
}
