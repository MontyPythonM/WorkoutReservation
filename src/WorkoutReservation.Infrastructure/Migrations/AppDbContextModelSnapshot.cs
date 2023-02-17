﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutReservation.Infrastructure.Persistence;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WorkoutReservation.Domain.Abstractions.BaseWorkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxParticipantNumber")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<int?>("WorkoutTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("WorkoutTypeId");

                    b.ToTable("BaseWorkouts", "WorkoutReservation");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseWorkout");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.ApplicationPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Permissions", "WorkoutReservation.Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "CreateInstructor"
                        },
                        new
                        {
                            Id = 2,
                            Name = "UpdateInstructor"
                        },
                        new
                        {
                            Id = 3,
                            Name = "DeleteInstructor"
                        },
                        new
                        {
                            Id = 4,
                            Name = "CreateWorkoutType"
                        },
                        new
                        {
                            Id = 5,
                            Name = "UpdateWorkoutType"
                        },
                        new
                        {
                            Id = 6,
                            Name = "DeleteWorkoutType"
                        },
                        new
                        {
                            Id = 7,
                            Name = "GetAllWorkoutTypeTags"
                        },
                        new
                        {
                            Id = 8,
                            Name = "CreateWorkoutTypeTag"
                        },
                        new
                        {
                            Id = 9,
                            Name = "UpdateWorkoutTypeTag"
                        },
                        new
                        {
                            Id = 10,
                            Name = "DeleteWorkoutTypeTag"
                        },
                        new
                        {
                            Id = 11,
                            Name = "GetRealWorkoutDetails"
                        },
                        new
                        {
                            Id = 12,
                            Name = "CreateRealWorkout"
                        },
                        new
                        {
                            Id = 13,
                            Name = "UpdateRealWorkout"
                        },
                        new
                        {
                            Id = 14,
                            Name = "DeleteRealWorkout"
                        },
                        new
                        {
                            Id = 15,
                            Name = "GetRepetitiveWorkouts"
                        },
                        new
                        {
                            Id = 16,
                            Name = "CreateRepetitiveWorkout"
                        },
                        new
                        {
                            Id = 17,
                            Name = "UpdateRepetitiveWorkout"
                        },
                        new
                        {
                            Id = 18,
                            Name = "DeleteRepetitiveWorkout"
                        },
                        new
                        {
                            Id = 19,
                            Name = "DeleteAllRepetitiveWorkouts"
                        },
                        new
                        {
                            Id = 20,
                            Name = "GenerateNewUpcomingWeek"
                        },
                        new
                        {
                            Id = 21,
                            Name = "OpenHangfireDashboard"
                        },
                        new
                        {
                            Id = 22,
                            Name = "GetOwnReservations"
                        },
                        new
                        {
                            Id = 23,
                            Name = "GetSomeoneReservations"
                        },
                        new
                        {
                            Id = 24,
                            Name = "CreateReservation"
                        },
                        new
                        {
                            Id = 25,
                            Name = "CancelReservation"
                        },
                        new
                        {
                            Id = 26,
                            Name = "UpdateReservationStatus"
                        },
                        new
                        {
                            Id = 27,
                            Name = "GetAllUsers"
                        },
                        new
                        {
                            Id = 28,
                            Name = "SetUserRole"
                        },
                        new
                        {
                            Id = 29,
                            Name = "DeleteUserAccount"
                        },
                        new
                        {
                            Id = 30,
                            Name = "DeleteOwnAccount"
                        },
                        new
                        {
                            Id = 31,
                            Name = "OpenAdministrationPage"
                        },
                        new
                        {
                            Id = 32,
                            Name = "CanSeeAdministrativeContent"
                        });
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Roles", "WorkoutReservation.Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SystemAdministrator"
                        },
                        new
                        {
                            Id = 2,
                            Name = "BusinessAdministrator"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Member"
                        });
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.ApplicationRolePermission", b =>
                {
                    b.Property<int>("ApplicationRoleId")
                        .HasColumnType("int");

                    b.Property<int>("ApplicationPermissionId")
                        .HasColumnType("int");

                    b.HasKey("ApplicationRoleId", "ApplicationPermissionId");

                    b.HasIndex("ApplicationPermissionId");

                    b.ToTable("RolesPermissions", "WorkoutReservation.Permissions");

                    b.HasData(
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 1
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 2
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 3
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 4
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 5
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 6
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 7
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 8
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 9
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 10
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 11
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 12
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 13
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 14
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 15
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 16
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 17
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 18
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 19
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 20
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 21
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 22
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 23
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 24
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 25
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 26
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 27
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 28
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 29
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 31
                        },
                        new
                        {
                            ApplicationRoleId = 1,
                            ApplicationPermissionId = 32
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 1
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 2
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 3
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 4
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 5
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 6
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 7
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 8
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 9
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 10
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 11
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 12
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 13
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 14
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 15
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 16
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 17
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 18
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 19
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 21
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 23
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 26
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 27
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 28
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 30
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 31
                        },
                        new
                        {
                            ApplicationRoleId = 2,
                            ApplicationPermissionId = 32
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 7
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 11
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 12
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 13
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 14
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 15
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 22
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 23
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 24
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 25
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 26
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 27
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 30
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 31
                        },
                        new
                        {
                            ApplicationRoleId = 3,
                            ApplicationPermissionId = 32
                        },
                        new
                        {
                            ApplicationRoleId = 4,
                            ApplicationPermissionId = 11
                        },
                        new
                        {
                            ApplicationRoleId = 4,
                            ApplicationPermissionId = 22
                        },
                        new
                        {
                            ApplicationRoleId = 4,
                            ApplicationPermissionId = 24
                        },
                        new
                        {
                            ApplicationRoleId = 4,
                            ApplicationPermissionId = 25
                        },
                        new
                        {
                            ApplicationRoleId = 4,
                            ApplicationPermissionId = 30
                        });
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AccountCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", "WorkoutReservation.Permissions");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.ApplicationUserRole", b =>
                {
                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApplicationRoleId")
                        .HasColumnType("int");

                    b.HasKey("ApplicationUserId", "ApplicationRoleId");

                    b.HasIndex("ApplicationRoleId");

                    b.ToTable("UsersRoles", "WorkoutReservation.Permissions");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Biography")
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Instructors", "WorkoutReservation");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RealWorkoutId")
                        .HasColumnType("int");

                    b.Property<int>("ReservationStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RealWorkoutId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations", "WorkoutReservation");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.WorkoutType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("Intensity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("WorkoutTypes", "WorkoutReservation");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.WorkoutTypeInstructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutTypeId")
                        .HasColumnType("int");

                    b.HasKey("InstructorId", "WorkoutTypeId");

                    b.HasIndex("WorkoutTypeId");

                    b.ToTable("WorkoutTypeInstructors", "WorkoutReservation");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.WorkoutTypeTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkoutTypeTags", "WorkoutReservation");
                });

            modelBuilder.Entity("WorkoutTypeWorkoutTypeTag", b =>
                {
                    b.Property<int>("WorkoutTypeTagsId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutTypesId")
                        .HasColumnType("int");

                    b.HasKey("WorkoutTypeTagsId", "WorkoutTypesId");

                    b.HasIndex("WorkoutTypesId");

                    b.ToTable("WorkoutTypeWorkoutTypeTag", "WorkoutReservation");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.RealWorkout", b =>
                {
                    b.HasBaseType("WorkoutReservation.Domain.Abstractions.BaseWorkout");

                    b.Property<int>("CurrentParticipantNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAutoGenerated")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("RealWorkout");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.RepetitiveWorkout", b =>
                {
                    b.HasBaseType("WorkoutReservation.Domain.Abstractions.BaseWorkout");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("RepetitiveWorkout");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Abstractions.BaseWorkout", b =>
                {
                    b.HasOne("WorkoutReservation.Domain.Entities.Instructor", "Instructor")
                        .WithMany("BaseWorkouts")
                        .HasForeignKey("InstructorId");

                    b.HasOne("WorkoutReservation.Domain.Entities.WorkoutType", "WorkoutType")
                        .WithMany("BaseWorkouts")
                        .HasForeignKey("WorkoutTypeId");

                    b.Navigation("Instructor");

                    b.Navigation("WorkoutType");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.ApplicationRolePermission", b =>
                {
                    b.HasOne("WorkoutReservation.Domain.Entities.ApplicationPermission", null)
                        .WithMany()
                        .HasForeignKey("ApplicationPermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservation.Domain.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("ApplicationRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.ApplicationUserRole", b =>
                {
                    b.HasOne("WorkoutReservation.Domain.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("ApplicationRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservation.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("WorkoutReservation.Domain.Entities.RealWorkout", "RealWorkout")
                        .WithMany("Reservations")
                        .HasForeignKey("RealWorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservation.Domain.Entities.ApplicationUser", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealWorkout");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.WorkoutTypeInstructor", b =>
                {
                    b.HasOne("WorkoutReservation.Domain.Entities.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservation.Domain.Entities.WorkoutType", "WorkoutType")
                        .WithMany()
                        .HasForeignKey("WorkoutTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("WorkoutType");
                });

            modelBuilder.Entity("WorkoutTypeWorkoutTypeTag", b =>
                {
                    b.HasOne("WorkoutReservation.Domain.Entities.WorkoutTypeTag", null)
                        .WithMany()
                        .HasForeignKey("WorkoutTypeTagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservation.Domain.Entities.WorkoutType", null)
                        .WithMany()
                        .HasForeignKey("WorkoutTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.Instructor", b =>
                {
                    b.Navigation("BaseWorkouts");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.WorkoutType", b =>
                {
                    b.Navigation("BaseWorkouts");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.RealWorkout", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
