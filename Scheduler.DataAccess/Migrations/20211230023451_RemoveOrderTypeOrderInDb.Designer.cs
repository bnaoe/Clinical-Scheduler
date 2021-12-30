﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scheduler.DataAccess;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211230023451_RemoveOrderTypeOrderInDb")]
    partial class RemoveOrderTypeOrderInDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Scheduler.Models.CodeSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CodeSets");
                });

            modelBuilder.Entity("Scheduler.Models.CodeValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CodeSetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CodeSetId");

                    b.ToTable("CodeValues");
                });

            modelBuilder.Entity("Scheduler.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("BMI")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Diastolic")
                        .HasColumnType("int");

                    b.Property<int>("DocStatusId")
                        .HasColumnType("int");

                    b.Property<int>("DocTypeId")
                        .HasColumnType("int");

                    b.Property<int>("EncounterId")
                        .HasColumnType("int");

                    b.Property<int?>("HeightFt")
                        .HasColumnType("int");

                    b.Property<float?>("HeightIn")
                        .HasColumnType("real");

                    b.Property<bool>("InError")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Narrative")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OxygenSaturation")
                        .HasColumnType("int");

                    b.Property<string>("PainLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PainScale")
                        .HasColumnType("int");

                    b.Property<string>("ProviderUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PulseRate")
                        .HasColumnType("int");

                    b.Property<int?>("Systolic")
                        .HasColumnType("int");

                    b.Property<float?>("Temperature")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("DocStatusId");

                    b.HasIndex("DocTypeId");

                    b.HasIndex("EncounterId");

                    b.HasIndex("ProviderUserId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Scheduler.Models.Encounter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("AdmitDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ConsentGiven")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DschDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FinancialNumAliasId")
                        .HasColumnType("int");

                    b.Property<string>("GroupNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuarantorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HealthPlanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InsuranceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("MemberNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<bool>("PrivacyNotice")
                        .HasColumnType("bit");

                    b.Property<string>("ProviderUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReasonForVisit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SchApptId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FinancialNumAliasId");

                    b.HasIndex("InsuranceId");

                    b.HasIndex("LocationId");

                    b.HasIndex("PatientId");

                    b.HasIndex("ProviderUserId");

                    b.HasIndex("SchApptId");

                    b.ToTable("Encounters");
                });

            modelBuilder.Entity("Scheduler.Models.FinancialNumAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("FinancialNumAliases");
                });

            modelBuilder.Entity("Scheduler.Models.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("Scheduler.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Scheduler.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AdminFreqId")
                        .HasColumnType("int");

                    b.Property<int>("AdminRouteId")
                        .HasColumnType("int");

                    b.Property<int>("AdminTimeId")
                        .HasColumnType("int");

                    b.Property<int>("EncounterId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Narrative")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderCatalogId")
                        .HasColumnType("int");

                    b.Property<string>("OrderDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderingDtTm")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderingUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdminFreqId");

                    b.HasIndex("AdminRouteId");

                    b.HasIndex("AdminTimeId");

                    b.HasIndex("EncounterId");

                    b.HasIndex("OrderCatalogId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("OrderingUserId");

                    b.HasIndex("PatientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Scheduler.Models.OrderCatalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CodeValueId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CodeValueId");

                    b.ToTable("OrderCatalogs");
                });

            modelBuilder.Entity("Scheduler.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Scheduler.Models.ProviderScheduleProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<bool>("Friday")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<bool>("Monday")
                        .HasColumnType("bit");

                    b.Property<string>("ProviderUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Saturday")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<bool>("Sunday")
                        .HasColumnType("bit");

                    b.Property<bool>("Thursday")
                        .HasColumnType("bit");

                    b.Property<bool>("Tuesday")
                        .HasColumnType("bit");

                    b.Property<bool>("Wednesday")
                        .HasColumnType("bit");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProviderUserId");

                    b.ToTable("ProviderScheduleProfiles");
                });

            modelBuilder.Entity("Scheduler.Models.SchAppt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApptStatusId")
                        .HasColumnType("int");

                    b.Property<int>("ApptTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("ProviderScheduleProfileId")
                        .HasColumnType("int");

                    b.Property<string>("RegistrarUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("end_date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("start_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApptStatusId");

                    b.HasIndex("ApptTypeId");

                    b.HasIndex("PatientId");

                    b.HasIndex("ProviderScheduleProfileId");

                    b.HasIndex("RegistrarUserId");

                    b.ToTable("SchAppts");
                });

            modelBuilder.Entity("Scheduler.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Suffix")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("LocationId");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Scheduler.Models.CodeValue", b =>
                {
                    b.HasOne("Scheduler.Models.CodeSet", "CodeSet")
                        .WithMany()
                        .HasForeignKey("CodeSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CodeSet");
                });

            modelBuilder.Entity("Scheduler.Models.Document", b =>
                {
                    b.HasOne("Scheduler.Models.CodeValue", "DocStatus")
                        .WithMany()
                        .HasForeignKey("DocStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.CodeValue", "DocType")
                        .WithMany()
                        .HasForeignKey("DocTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.Encounter", "Encounter")
                        .WithMany()
                        .HasForeignKey("EncounterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.ApplicationUser", "ProviderUser")
                        .WithMany()
                        .HasForeignKey("ProviderUserId");

                    b.Navigation("DocStatus");

                    b.Navigation("DocType");

                    b.Navigation("Encounter");

                    b.Navigation("ProviderUser");
                });

            modelBuilder.Entity("Scheduler.Models.Encounter", b =>
                {
                    b.HasOne("Scheduler.Models.FinancialNumAlias", "FinancialNumAlias")
                        .WithMany()
                        .HasForeignKey("FinancialNumAliasId");

                    b.HasOne("Scheduler.Models.Insurance", "Insurance")
                        .WithMany()
                        .HasForeignKey("InsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.ApplicationUser", "ProviderUser")
                        .WithMany()
                        .HasForeignKey("ProviderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.SchAppt", "SchAppt")
                        .WithMany()
                        .HasForeignKey("SchApptId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FinancialNumAlias");

                    b.Navigation("Insurance");

                    b.Navigation("Location");

                    b.Navigation("Patient");

                    b.Navigation("ProviderUser");

                    b.Navigation("SchAppt");
                });

            modelBuilder.Entity("Scheduler.Models.Order", b =>
                {
                    b.HasOne("Scheduler.Models.CodeValue", "AdminFreq")
                        .WithMany()
                        .HasForeignKey("AdminFreqId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.CodeValue", "AdminRoute")
                        .WithMany()
                        .HasForeignKey("AdminRouteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.CodeValue", "AdminTime")
                        .WithMany()
                        .HasForeignKey("AdminTimeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.Encounter", "Encounter")
                        .WithMany()
                        .HasForeignKey("EncounterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.OrderCatalog", "OrderCatalog")
                        .WithMany()
                        .HasForeignKey("OrderCatalogId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.CodeValue", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.ApplicationUser", "OrderingUser")
                        .WithMany()
                        .HasForeignKey("OrderingUserId");

                    b.HasOne("Scheduler.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AdminFreq");

                    b.Navigation("AdminRoute");

                    b.Navigation("AdminTime");

                    b.Navigation("Encounter");

                    b.Navigation("OrderCatalog");

                    b.Navigation("OrderStatus");

                    b.Navigation("OrderingUser");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Scheduler.Models.OrderCatalog", b =>
                {
                    b.HasOne("Scheduler.Models.CodeValue", "CodeValue")
                        .WithMany()
                        .HasForeignKey("CodeValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CodeValue");
                });

            modelBuilder.Entity("Scheduler.Models.ProviderScheduleProfile", b =>
                {
                    b.HasOne("Scheduler.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.ApplicationUser", "ProviderUser")
                        .WithMany()
                        .HasForeignKey("ProviderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("ProviderUser");
                });

            modelBuilder.Entity("Scheduler.Models.SchAppt", b =>
                {
                    b.HasOne("Scheduler.Models.CodeValue", "ApptStatus")
                        .WithMany()
                        .HasForeignKey("ApptStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.CodeValue", "ApptType")
                        .WithMany()
                        .HasForeignKey("ApptTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.ProviderScheduleProfile", "ProviderScheduleProfile")
                        .WithMany()
                        .HasForeignKey("ProviderScheduleProfileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scheduler.Models.ApplicationUser", "RegistrarUser")
                        .WithMany()
                        .HasForeignKey("RegistrarUserId");

                    b.Navigation("ApptStatus");

                    b.Navigation("ApptType");

                    b.Navigation("Patient");

                    b.Navigation("ProviderScheduleProfile");

                    b.Navigation("RegistrarUser");
                });

            modelBuilder.Entity("Scheduler.Models.ApplicationUser", b =>
                {
                    b.HasOne("Scheduler.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });
#pragma warning restore 612, 618
        }
    }
}
