﻿// <auto-generated />
using System;
using DUR_Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DUR_Application.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230916195512_MagazineId_SpareParts")]
    partial class MagazineId_SpareParts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DUR_Application.Entities.Lane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Describiton")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lanes");
                });

            modelBuilder.Entity("DUR_Application.Entities.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LaneID")
                        .HasColumnType("int");

                    b.Property<string>("LaneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LaneID");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("DUR_Application.Entities.Magazine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Magazines");
                });

            modelBuilder.Entity("DUR_Application.Entities.MalfunctionRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LaneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("LayoverTime")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestStatusId")
                        .HasColumnType("int");

                    b.Property<int>("RequestTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("WorkTime")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MachineId");

                    b.HasIndex("RequestStatusId");

                    b.HasIndex("RequestTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("MalfunctionRequests");
                });

            modelBuilder.Entity("DUR_Application.Entities.RequestStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("RequestStatus");
                });

            modelBuilder.Entity("DUR_Application.Entities.RequestType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RequestTypes");
                });

            modelBuilder.Entity("DUR_Application.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DUR_Application.Entities.SparePart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MagazineId")
                        .HasColumnType("int");

                    b.Property<int?>("MalfunctionRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MagazineId");

                    b.HasIndex("MalfunctionRequestId");

                    b.ToTable("SpareParts");
                });

            modelBuilder.Entity("DUR_Application.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DUR_Application.Entities.Machine", b =>
                {
                    b.HasOne("DUR_Application.Entities.Lane", "Lane")
                        .WithMany("Machines")
                        .HasForeignKey("LaneID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lane");
                });

            modelBuilder.Entity("DUR_Application.Entities.MalfunctionRequest", b =>
                {
                    b.HasOne("DUR_Application.Entities.Machine", "Machine")
                        .WithMany("Malfunctions")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUR_Application.Entities.RequestStatus", "RequestStatus")
                        .WithMany()
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUR_Application.Entities.RequestType", "RequestType")
                        .WithMany("MalfunctionRequests")
                        .HasForeignKey("RequestTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUR_Application.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("CreatedBy");

                    b.Navigation("Machine");

                    b.Navigation("RequestStatus");

                    b.Navigation("RequestType");
                });

            modelBuilder.Entity("DUR_Application.Entities.SparePart", b =>
                {
                    b.HasOne("DUR_Application.Entities.Magazine", "Magazine")
                        .WithMany("Parts")
                        .HasForeignKey("MagazineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DUR_Application.Entities.MalfunctionRequest", null)
                        .WithMany("SpareParts")
                        .HasForeignKey("MalfunctionRequestId");

                    b.Navigation("Magazine");
                });

            modelBuilder.Entity("DUR_Application.Entities.User", b =>
                {
                    b.HasOne("DUR_Application.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DUR_Application.Entities.Lane", b =>
                {
                    b.Navigation("Machines");
                });

            modelBuilder.Entity("DUR_Application.Entities.Machine", b =>
                {
                    b.Navigation("Malfunctions");
                });

            modelBuilder.Entity("DUR_Application.Entities.Magazine", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("DUR_Application.Entities.MalfunctionRequest", b =>
                {
                    b.Navigation("SpareParts");
                });

            modelBuilder.Entity("DUR_Application.Entities.RequestType", b =>
                {
                    b.Navigation("MalfunctionRequests");
                });

            modelBuilder.Entity("DUR_Application.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
