﻿// <auto-generated />
using System;
using InnoClinic.Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InnoClinic.Infrastructure.Migrations
{
    [DbContext(typeof(ClinicDbContext))]
    [Migration("20230824161359_AddedManyToManyBetweenOfficeAndUser")]
    partial class AddedManyToManyBetweenOfficeAndUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InnoClinic.Domain.Entities.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("House number");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("OfficeNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Office number");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("RegistryPhoneNumber")
                        .HasColumnType("bigint")
                        .HasColumnName("Registry phone number");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Offices", (string)null);
                });

            modelBuilder.Entity("InnoClinic.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("E-mail");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("OfficeUser", b =>
                {
                    b.Property<int>("OfficeListId")
                        .HasColumnType("int");

                    b.Property<int>("UserListId")
                        .HasColumnType("int");

                    b.HasKey("OfficeListId", "UserListId");

                    b.HasIndex("UserListId");

                    b.ToTable("OfficeUser");
                });

            modelBuilder.Entity("OfficeUser", b =>
                {
                    b.HasOne("InnoClinic.Domain.Entities.Office", null)
                        .WithMany()
                        .HasForeignKey("OfficeListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InnoClinic.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}