using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SFA.Models;

namespace SFA.Entities
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

        public virtual DbSet<TblAccomodationBookingNta> TblAccomodationBookingNta { get; set; }
        public virtual DbSet<TblAppointmentNta> TblAppointmentNta { get; set; }
        public virtual DbSet<TblAttributeNta> TblAttributeNta { get; set; }
        public virtual DbSet<TblAttributeTypeNta> TblAttributeTypeNta { get; set; }
        public virtual DbSet<TblChurchAccommodationNta> TblChurchAccommodationNta { get; set; }
        public virtual DbSet<TblChurchAttributeNta> TblChurchAttributeNta { get; set; }
        public virtual DbSet<TblChurchNta> TblChurchNta { get; set; }
        public virtual DbSet<TblChurchServiceTimeNta> TblChurchServiceTimeNta { get; set; }
        public virtual DbSet<TblCountryNta> TblCountryNta { get; set; }
        public virtual DbSet<TblDistrictNta> TblDistrictNta { get; set; }
        public virtual DbSet<TblMacroScheduleDetailsNta> TblMacroScheduleDetailsNta { get; set; }
        public virtual DbSet<TblMacroScheduleNta> TblMacroScheduleNta { get; set; }
        public virtual DbSet<TblMenuGroupNta> TblMenuGroupNta { get; set; }
        public virtual DbSet<TblMenuNta> TblMenuNta { get; set; }
        public virtual DbSet<TblNotificationNta> TblNotificationNta { get; set; }
        public virtual DbSet<TblRoleMenuNta> TblRoleMenuNta { get; set; }
        public virtual DbSet<TblRoleNta> TblRoleNta { get; set; }
        public virtual DbSet<TblSectionNta> TblSectionNta { get; set; }
        public virtual DbSet<TblServiceTypeNta> TblServiceTypeNta { get; set; }
        public virtual DbSet<TblStateDistrictNta> TblStateDistrictNta { get; set; }
        public virtual DbSet<TblStateNta> TblStateNta { get; set; }
        public virtual DbSet<TblUserAttributeNta> TblUserAttributeNta { get; set; }
        public virtual DbSet<TblUserChurchNta> TblUserChurchNta { get; set; }
        public virtual DbSet<TblUserLogNta> TblUserLogNta { get; set; }
        public virtual DbSet<TblUserNta> TblUserNta { get; set; }
        public virtual DbSet<TblUserPasswordNta> TblUserPasswordNta { get; set; }

        public DbSet<MacroScheduleDetailCount> MacroScheduleCounts { get; set; }

        public DbSet<MacroScheduleDetailThirtyDayCount> MacroScheduleDetailThirtyDayCount { get; set; }

        public DbSet<ServiceCountOneYear> ServiceCountOneYear { get; set; }

        public DbSet<MissionarySummary> MissionarySummary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceCountOneYear>(entity =>

            {

                entity.HasNoKey();

                entity.ToView("TotalServicesLastYear");

            });

            modelBuilder.Entity<MacroScheduleDetailThirtyDayCount>(entity =>

            {

                entity.HasNoKey();

                entity.ToView("MacroScheduleDeatailsNextThirtyDays");

            });

            modelBuilder.Entity<MacroScheduleDetailCount>(entity =>

            { 
                entity.HasNoKey();

                entity.ToView("MacroScheduleDetailCounts");

            });

            modelBuilder.Entity<MissionarySummary>(entity =>

            {
                entity.HasNoKey();

                entity.ToView("MissionarySummary");

            });

            modelBuilder.Entity<TblAccomodationBookingNta>(entity =>
            {
                entity.HasOne(d => d.Accomodation)
                    .WithMany(p => p.TblAccomodationBookingNta)
                    .HasForeignKey(d => d.AccomodationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_AccomodationBooking_NTA_AccomodationId_Tbl_ChurchAccommodation_NTA_ID");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.TblAccomodationBookingNtaApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_Tbl_AccomodationBooking_NTA_ApprovedBy_Tbl_User_NTA_ID");

                entity.HasOne(d => d.Church)
                    .WithMany(p => p.TblAccomodationBookingNta)
                    .HasForeignKey(d => d.ChurchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_AccomodationBooking_NTA_ChurchId_Tbl_Church_NTA_ID");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.TblAccomodationBookingNta)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_AccomodationBooking_NTA_DistrictId_Tbl_District_NTA_ID");

                entity.HasOne(d => d.RequestedUser)
                    .WithMany(p => p.TblAccomodationBookingNtaRequestedUser)
                    .HasForeignKey(d => d.RequestedUserId)
                    .HasConstraintName("FK_Tbl_AccomodationBooking_NTA_UserId_Tbl_User_NTA_ID");
            });
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

                entity.HasOne(d => d.Church)
                    .WithMany(p => p.TblAppointmentNta)
                    .HasForeignKey(d => d.ChurchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_ChurchId_Tbl_Church_NTA_ID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblAppointmentNtaCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_CreatedBy_Tbl_User_NTA_ID");

                entity.HasOne(d => d.ServiceType)
                   .WithMany(p => p.TblAppointmentNta)
                   .HasForeignKey(d => d.ServiceTypeId)
                   .HasConstraintName("FK_Tbl_Appointment_NTA_SERVICE_TYPE_ID_Tbl_ServiceType_NTA_ID");

                entity.HasOne(d => d.MacroScheduleDetail)
                    .WithMany(p => p.TblAppointmentNta)
                    .HasForeignKey(d => d.MacroScheduleDetailId)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_MacroScheduleDetailId_Tbl_MacroScheduleDetails_NTA_ID");

                entity.HasOne(d => d.SubmittedByNavigation)
                    .WithMany(p => p.TblAppointmentNtaSubmittedByNavigation)
                    .HasForeignKey(d => d.SubmittedBy)
                    .HasConstraintName("FK_Tbl_Appointment_NTA_SubmittedBy_Tbl_User_NTA_ID");
            });


            modelBuilder.Entity<TblAttributeNta>(entity =>
            {
                entity.HasOne(d => d.AttributeType)
                    .WithMany(p => p.TblAttributeNta)
                    .HasForeignKey(d => d.AttributeTypeId)
                    .HasConstraintName("FK_Tbl_Attribute_NTA_AttributeTypeId_Tbl_AttributeType_NTA_ID");
            });

            modelBuilder.Entity<TblChurchAccommodationNta>(entity =>
            {
                entity.HasOne(d => d.Church)
                    .WithMany(p => p.TblChurchAccommodationNta)
                    .HasForeignKey(d => d.ChurchId)
                    .HasConstraintName("FK_Tbl_ChurchAccommodation_NTA_ChurchID_Tbl_Church_NTA_ID");
            });

            modelBuilder.Entity<TblChurchAttributeNta>(entity =>
            {
                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.TblChurchAttributeNta)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_Tbl_ChurchAttribute_NTA_AttributeId_Tbl_Attribute_NTA_ID");

                entity.HasOne(d => d.Church)
                    .WithMany(p => p.TblChurchAttributeNta)
                    .HasForeignKey(d => d.ChurchId)
                    .HasConstraintName("FK_Tbl_ChurchAttribute_NTA_ChurchId_Tbl_Church_NTA_ID");
            });

            modelBuilder.Entity<TblChurchNta>(entity =>
            {
                entity.HasOne(d => d.District)
                    .WithMany(p => p.TblChurchNta)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Tbl_Church_NTA_DistrictID_Tbl_District_NTA_ID");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TblChurchNta)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Church_NTA_SectionID_Tbl_Section_NTA_ID");
            });

            modelBuilder.Entity<TblChurchServiceTimeNta>(entity =>
            {
                entity.HasOne(d => d.Church)
                    .WithMany(p => p.TblChurchServiceTimeNta)
                    .HasForeignKey(d => d.ChurchId)
                    .HasConstraintName("FK_Tbl_ChurchServiceTime_NTA_ChurchId_Tbl_Church_NTA_ID");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.TblChurchServiceTimeNta)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_Tbl_ChurchServiceTime_NTA_ServiceTypeId_Tbl_ServiceTypeId_NTA_ID");
            });

            modelBuilder.Entity<TblCountryNta>(entity =>
            {
                entity.Property(e => e.Code).HasComputedColumnSql("('C'+format([CodeVal],'000'))");
            });

            modelBuilder.Entity<TblDistrictNta>(entity =>
            {
                entity.Property(e => e.Code).HasComputedColumnSql("('D'+format([CodeVal],'000'))");
            });

            modelBuilder.Entity<TblMacroScheduleDetailsNta>(entity =>
            {
                entity.HasOne(d => d.ApprovedRejectByNavigation)
                    .WithMany(p => p.TblMacroScheduleDetailsNtaApprovedRejectByNavigation)
                    .HasForeignKey(d => d.ApprovedRejectBy)
                    .HasConstraintName("FK_Tbl_MacroScheduleDetails_NTA_ApprovedRejectBy_Tbl_User_NTA_ID");

                entity.HasOne(d => d.MacroSchedule)
                    .WithMany(p => p.TblMacroScheduleDetailsNta)
                    .HasForeignKey(d => d.MacroScheduleId)
                    .HasConstraintName("FK_Tbl_MacroScheduleDetails_NTA_MacroScheduleId_Tbl_MacroSchedule_NTA_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblMacroScheduleDetailsNtaUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tbl_MacroScheduleDetails_NTA_UserId_Tbl_User_NTA_ID");
            });

            modelBuilder.Entity<TblMenuNta>(entity =>
            {
                entity.HasOne(d => d.MenuGroup)
                    .WithMany(p => p.TblMenuNta)
                    .HasForeignKey(d => d.MenuGroupId)
                    .HasConstraintName("FK_Tbl_Menu_NTA_MenuGroupID_Tbl_Menu_Group_NTA_ID");
            });

            modelBuilder.Entity<TblRoleMenuNta>(entity =>
            {
                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.TblRoleMenuNta)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_Tbl_RoleMenu_NTA_MenuID_Tbl_Menu_NTA_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblRoleMenuNta)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Tbl_RoleMenu_NTA_RoleID_Tbl_Menu_Role_NTA_ID");
            });

            modelBuilder.Entity<TblSectionNta>(entity =>
            {
                entity.HasOne(d => d.District)
                    .WithMany(p => p.TblSectionNta)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Tbl_Section_NTA_DistrictID_Tbl_District_NTA_ID");
            });

            modelBuilder.Entity<TblStateDistrictNta>(entity =>
            {
                entity.HasOne(d => d.District)
                    .WithMany(p => p.TblStateDistrictNta)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Tbl_StateDistrict_NTA_DistrictId_Tbl_District_NTA_ID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.TblStateDistrictNta)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Tbl_StateDistrict_NTA_StateId_Tbl_State_NTA_ID");
            });

            modelBuilder.Entity<TblStateNta>(entity =>
            {
                entity.Property(e => e.Code).HasComputedColumnSql("('S'+format([CodeVal],'000'))");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TblStateNta)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Tbl_State_NTA_CountryID_Tbl_Country_NTA_ID");
            });

            modelBuilder.Entity<TblUserAttributeNta>(entity =>
            {
                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.TblUserAttributeNta)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_Tbl_UserAttribute_NTA_AttributeId_Tbl_Attribute_NTA_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserAttributeNta)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_UserAttribute_NTA_UserId_Tbl_User_NTA_ID");
            });

            modelBuilder.Entity<TblUserLogNta>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserLogNta)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_UserLog_NTA_UserId_Tbl_User_NTA_ID");
            });

            modelBuilder.Entity<TblUserNta>(entity =>
            {
               
                entity.Property(e => e.WorkPhoneNo).HasMaxLength(50);
                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TblUserNta)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Tbl_User_NTA_CountryId_Tbl_Country_NTA_ID");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.TblUserNta)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Tbl_User_NTA_DistrictId_Tbl_Distrcit_NTA_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUserNta)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Tbl_User_NTA_RoleId_Tbl_Role_NTA_ID");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TblUserNta)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tbl_User_NTA_AttributeId_Tbl_Section_NTA_ID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.TblUserNta)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tbl_User_NTA_StateId_Tbl_State_NTA_ID");
            });

            modelBuilder.Entity<TblUserPasswordNta>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserPasswordNta)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_UserPassword_NTA_UserId_Tbl_User_NTA_ID");
            });
        }
    }
}
