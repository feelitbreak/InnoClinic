﻿// <auto-generated />
using InnoClinic.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InnoClinic.Infrastructure.Migrations
{
    [DbContext(typeof(ClinicDbContext))]
    [Migration("20230815202658_RemovedReenteredPasswordColumn")]
    partial class RemovedReenteredPasswordColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InnoClinic.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("")
                        .HasColumnName("E-mail");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasDefaultValue("");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
