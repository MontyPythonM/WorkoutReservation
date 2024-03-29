﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutReservation.Infrastructure.Persistence;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220614181037_Add-reservation")]
    partial class Addreservation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WorkoutReservation.Domain.Common.BaseWorkout", b =>
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

                    b.Property<int>("MaxParticipianNumber")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<int?>("WorkoutTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("WorkoutTypeId");

                    b.ToTable("Workouts", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseWorkout");
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

                    b.ToTable("Instructors");
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

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AccountCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConfirmationToken")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
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

                    b.ToTable("WorkoutTypes");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.WorkoutTypeInstructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutTypeId")
                        .HasColumnType("int");

                    b.HasKey("InstructorId", "WorkoutTypeId");

                    b.HasIndex("WorkoutTypeId");

                    b.ToTable("WorkoutTypeInstructors");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.WorkoutTypeTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkoutTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutTypeId");

                    b.ToTable("WorkoutTypeTags");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.RealWorkout", b =>
                {
                    b.HasBaseType("WorkoutReservation.Domain.Common.BaseWorkout");

                    b.Property<int>("CurrentParticipianNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAutoGenerated")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("RealWorkout");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.RepetitiveWorkout", b =>
                {
                    b.HasBaseType("WorkoutReservation.Domain.Common.BaseWorkout");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("RepetitiveWorkout");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Common.BaseWorkout", b =>
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

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("WorkoutReservation.Domain.Entities.RealWorkout", "RealWorkout")
                        .WithMany("Reservations")
                        .HasForeignKey("RealWorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservation.Domain.Entities.User", "User")
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
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WorkoutReservation.Domain.Entities.WorkoutType", "WorkoutType")
                        .WithMany()
                        .HasForeignKey("WorkoutTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("WorkoutType");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.WorkoutTypeTag", b =>
                {
                    b.HasOne("WorkoutReservation.Domain.Entities.WorkoutType", "WorkoutType")
                        .WithMany("WorkoutTypeTags")
                        .HasForeignKey("WorkoutTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkoutType");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.Instructor", b =>
                {
                    b.Navigation("BaseWorkouts");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.User", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.WorkoutType", b =>
                {
                    b.Navigation("BaseWorkouts");

                    b.Navigation("WorkoutTypeTags");
                });

            modelBuilder.Entity("WorkoutReservation.Domain.Entities.RealWorkout", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
