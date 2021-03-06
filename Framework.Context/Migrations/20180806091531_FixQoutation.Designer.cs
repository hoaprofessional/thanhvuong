﻿// <auto-generated />
using Framework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Framework.Context.Migrations
{
    [DbContext(typeof(FrameworkDbContext))]
    [Migration("20180806091531_FixQoutation")]
    partial class FixQoutation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Framework.Models.Configuration.CommonConfiguration", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active");

                    b.Property<string>("CompanyName");

                    b.Property<string>("Contact");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<string>("HeadQuarter");

                    b.Property<string>("Hotline");

                    b.Property<bool?>("IsTest");

                    b.Property<string>("Logo");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Note");

                    b.Property<bool?>("Protected");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.ToTable("CommonConfigurations");
                });

            modelBuilder.Entity("Framework.Models.NotificationManagement.Notification", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<string>("Icon");

                    b.Property<bool>("IsRead");

                    b.Property<bool?>("IsTest");

                    b.Property<string>("Link");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Note");

                    b.Property<bool?>("Protected");

                    b.Property<string>("StaffId");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.Client", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<string>("Address")
                        .HasMaxLength(5000);

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<string>("Email")
                        .HasMaxLength(1000);

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Note");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(450);

                    b.Property<bool?>("Protected");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<string>("Decription");

                    b.Property<string>("Images")
                        .HasMaxLength(2000);

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Note");

                    b.Property<decimal>("Price");

                    b.Property<bool?>("Protected");

                    b.Property<string>("Size");

                    b.Property<string>("Unit")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.Qoutation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active");

                    b.Property<string>("ClientId")
                        .HasMaxLength(450);

                    b.Property<string>("ConfirmStaffId")
                        .HasMaxLength(450);

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<string>("DeliveryPlace")
                        .HasMaxLength(1000);

                    b.Property<decimal>("EstimatedInstallationTime");

                    b.Property<bool?>("IsTest");

                    b.Property<string>("ManagerId")
                        .HasMaxLength(450);

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Note");

                    b.Property<string>("PaymentMethod");

                    b.Property<bool?>("Protected");

                    b.Property<string>("QoutationStatusId");

                    b.Property<string>("QouteStaffId")
                        .HasMaxLength(450);

                    b.Property<string>("RejectReason");

                    b.Property<string>("SalesAdminId");

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ConfirmStaffId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("QoutationStatusId");

                    b.HasIndex("QouteStaffId");

                    b.HasIndex("SalesAdminId");

                    b.ToTable("Qoutation");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.QoutationDetail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Note");

                    b.Property<string>("ProductDetail");

                    b.Property<string>("ProductId")
                        .HasMaxLength(450);

                    b.Property<string>("ProductName");

                    b.Property<string>("ProductNote");

                    b.Property<int>("ProductQuantity");

                    b.Property<string>("ProductSize");

                    b.Property<string>("ProductUnit");

                    b.Property<bool?>("Protected");

                    b.Property<int>("QoutationId");

                    b.Property<decimal>("TotalPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");

                    b.Property<decimal>("UnitPrice");

                    b.Property<double>("VAT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("QoutationId");

                    b.ToTable("QoutationDetail");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.QoutationEvent", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Note");

                    b.Property<bool?>("Protected");

                    b.Property<int>("QoutationId");

                    b.Property<string>("QoutationStatusId")
                        .HasMaxLength(450);

                    b.Property<string>("StaffId")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("QoutationId");

                    b.HasIndex("QoutationStatusId");

                    b.HasIndex("StaffId");

                    b.ToTable("QoutationEvent");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.QoutationStatus", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Name")
                        .HasMaxLength(2000);

                    b.Property<string>("Note");

                    b.Property<bool?>("Protected");

                    b.HasKey("Id");

                    b.ToTable("QoutationStatus");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.Staff", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<string>("Address")
                        .HasMaxLength(2000);

                    b.Property<string>("Career")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<string>("IdentityCard")
                        .HasMaxLength(450);

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Note");

                    b.Property<bool?>("Protected");

                    b.Property<string>("UserId")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("Framework.Models.TaskManagement.Priority", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<string>("Color");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Note");

                    b.Property<int>("PriorityValue");

                    b.Property<bool?>("Protected");

                    b.HasKey("Id");

                    b.ToTable("Priority","Task");
                });

            modelBuilder.Entity("Framework.Models.TaskManagement.Task", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<string>("AssignToId")
                        .HasMaxLength(450);

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<DateTime>("DateBegin");

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Note");

                    b.Property<string>("PriorityId")
                        .HasMaxLength(450);

                    b.Property<bool?>("Protected");

                    b.Property<string>("TaskStatusId")
                        .HasMaxLength(450);

                    b.Property<string>("WorkId")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("AssignToId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("TaskStatusId");

                    b.HasIndex("WorkId");

                    b.ToTable("Task","Task");
                });

            modelBuilder.Entity("Framework.Models.TaskManagement.TaskStatus", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<string>("Decription")
                        .HasMaxLength(200);

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Note");

                    b.Property<bool?>("Protected");

                    b.HasKey("Id");

                    b.ToTable("TaskStatus","Task");
                });

            modelBuilder.Entity("Framework.Models.TaskManagement.Work", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<DateTime>("DateBegin");

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Note");

                    b.Property<string>("PriorityId")
                        .HasMaxLength(450);

                    b.Property<bool?>("Protected");

                    b.Property<DateTime>("TimeExpired");

                    b.Property<string>("WorkStatusId")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("PriorityId");

                    b.HasIndex("WorkStatusId");

                    b.ToTable("Work","Task");
                });

            modelBuilder.Entity("Framework.Models.TaskManagement.WorkStatus", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<bool?>("Active");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<string>("Decription")
                        .HasMaxLength(200);

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Note");

                    b.Property<bool?>("Protected");

                    b.HasKey("Id");

                    b.ToTable("WorkStatus","Task");
                });

            modelBuilder.Entity("Framework.Models.UserManagement.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool?>("Active");

                    b.Property<string>("Address");

                    b.Property<string>("Avatar");

                    b.Property<string>("Carrer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("IdentityCardNumber");

                    b.Property<bool?>("IsBanned");

                    b.Property<bool?>("IsTest");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Note");

                    b.Property<string>("ObjectId")
                        .HasMaxLength(450);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<bool?>("Protected");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ObjectId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Framework.Models.UserManagement.Permission", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("Framework.Models.UserManagement.UserObject", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("DefaultCarrer");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("RoleId")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("UserObject");
                });

            modelBuilder.Entity("Framework.Models.Utils.CacheData", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<bool>("Expired");

                    b.Property<DateTime>("ExpiredDate");

                    b.Property<bool?>("IsTest");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Note");

                    b.Property<bool?>("Protected");

                    b.Property<string>("Value");

                    b.HasKey("Key");

                    b.ToTable("CacheDatas");
                });

            modelBuilder.Entity("Framework.Models.Utils.Logger", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<string>("CreationUserName");

                    b.Property<bool?>("IsTest");

                    b.Property<string>("LogType");

                    b.Property<DateTime?>("ModifiedTime");

                    b.Property<string>("ModifiedUserName");

                    b.Property<string>("Note");

                    b.Property<bool?>("Protected");

                    b.HasKey("Id");

                    b.ToTable("Loggers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.Qoutation", b =>
                {
                    b.HasOne("Framework.Models.QoutationManagement.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("Framework.Models.QoutationManagement.Staff", "ConfirmStaff")
                        .WithMany()
                        .HasForeignKey("ConfirmStaffId");

                    b.HasOne("Framework.Models.QoutationManagement.Staff", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");

                    b.HasOne("Framework.Models.QoutationManagement.QoutationStatus", "QoutationStatus")
                        .WithMany()
                        .HasForeignKey("QoutationStatusId");

                    b.HasOne("Framework.Models.QoutationManagement.Staff", "QouteStaff")
                        .WithMany()
                        .HasForeignKey("QouteStaffId");

                    b.HasOne("Framework.Models.QoutationManagement.Staff", "SalesAdmin")
                        .WithMany()
                        .HasForeignKey("SalesAdminId");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.QoutationDetail", b =>
                {
                    b.HasOne("Framework.Models.QoutationManagement.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("Framework.Models.QoutationManagement.Qoutation", "Qoutation")
                        .WithMany("QoutationDetails")
                        .HasForeignKey("QoutationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.QoutationEvent", b =>
                {
                    b.HasOne("Framework.Models.QoutationManagement.Qoutation", "Qoutation")
                        .WithMany()
                        .HasForeignKey("QoutationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Framework.Models.QoutationManagement.QoutationStatus", "QoutationStatus")
                        .WithMany()
                        .HasForeignKey("QoutationStatusId");

                    b.HasOne("Framework.Models.QoutationManagement.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("Framework.Models.QoutationManagement.Staff", b =>
                {
                    b.HasOne("Framework.Models.UserManagement.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Framework.Models.TaskManagement.Task", b =>
                {
                    b.HasOne("Framework.Models.UserManagement.ApplicationUser", "AssignTo")
                        .WithMany()
                        .HasForeignKey("AssignToId");

                    b.HasOne("Framework.Models.TaskManagement.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId");

                    b.HasOne("Framework.Models.TaskManagement.TaskStatus", "TaskStatus")
                        .WithMany()
                        .HasForeignKey("TaskStatusId");

                    b.HasOne("Framework.Models.TaskManagement.Work", "Work")
                        .WithMany()
                        .HasForeignKey("WorkId");
                });

            modelBuilder.Entity("Framework.Models.TaskManagement.Work", b =>
                {
                    b.HasOne("Framework.Models.TaskManagement.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId");

                    b.HasOne("Framework.Models.TaskManagement.WorkStatus", "WorkStatus")
                        .WithMany()
                        .HasForeignKey("WorkStatusId");
                });

            modelBuilder.Entity("Framework.Models.UserManagement.ApplicationUser", b =>
                {
                    b.HasOne("Framework.Models.UserManagement.UserObject", "UserObject")
                        .WithMany()
                        .HasForeignKey("ObjectId");
                });

            modelBuilder.Entity("Framework.Models.UserManagement.UserObject", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Framework.Models.UserManagement.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Framework.Models.UserManagement.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Framework.Models.UserManagement.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Framework.Models.UserManagement.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
