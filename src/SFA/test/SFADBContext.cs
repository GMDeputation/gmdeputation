using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SFA.test
{
    public partial class SFADBContext : DbContext
    {
        public SFADBContext()
        {
        }

        public SFADBContext(DbContextOptions<SFADBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAppointmentNta> TblAppointmentNta { get; set; }
        public virtual DbSet<TblMacroScheduleDetailsNta> TblMacroScheduleDetailsNta { get; set; }
        public virtual DbSet<TblServiceTypeNta> TblServiceTypeNta { get; set; }
        public virtual DbSet<TblUserNta> TblUserNta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=72.167.225.108\\\\\\\\SQLEXPRESS,1433;Database=SFADB;User Id=sa;Password=Db@dm1n;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAppointmentNta>(entity =>
            {
                entity.Property(e => e.ServiceTypeId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcceptByPastorByNavigation)
                    .WithMany(p => p.TblAppointmentNtaAcceptByPastorByNavigation)
                    .HasForeignKey(d => d.AcceptByPastorBy)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_AcceptByPastorBy_Tbl_User_NTA_ID");

                entity.HasOne(d => d.AcceptMissionaryByNavigation)
                    .WithMany(p => p.TblAppointmentNtaAcceptMissionaryByNavigation)
                    .HasForeignKey(d => d.AcceptMissionaryBy)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_AcceptMissionaryBy_Tbl_User_NTA_ID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblAppointmentNtaCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_CreatedBy_Tbl_User_NTA_ID");

                entity.HasOne(d => d.MacroScheduleDetail)
                    .WithMany(p => p.TblAppointmentNta)
                    .HasForeignKey(d => d.MacroScheduleDetailId)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_MacroScheduleDetailId_Tbl_MacroScheduleDetails_NTA_ID");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.TblAppointmentNta)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_SERVICE_TYPE_ID_Tbl_ServiceType_NTA_ID");

                entity.HasOne(d => d.SubmittedByNavigation)
                    .WithMany(p => p.TblAppointmentNtaSubmittedByNavigation)
                    .HasForeignKey(d => d.SubmittedBy)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_SubmittedBy_Tbl_User_NTA_ID");
            });

            modelBuilder.Entity<TblMacroScheduleDetailsNta>(entity =>
            {
                entity.HasOne(d => d.ApprovedRejectByNavigation)
                    .WithMany(p => p.TblMacroScheduleDetailsNtaApprovedRejectByNavigation)
                    .HasForeignKey(d => d.ApprovedRejectBy)
                    .HasConstraintName("FK_Tbl_MacroScheduleDetails_NTA_ApprovedRejectBy_Tbl_User_NTA_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblMacroScheduleDetailsNtaUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tbl_MacroScheduleDetails_NTA_UserId_Tbl_User_NTA_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
