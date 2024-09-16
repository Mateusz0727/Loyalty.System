﻿// <auto-generated />
using System;
using Loyalty.System.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Loyalty.System.Data.Migrations
{
    [DbContext(typeof(BaseContext))]
    [Migration("20240815131551_create_admin_account")]
    partial class create_admin_account
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Loyalty.System.Data.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreatedUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModifiedUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("GivenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreatedUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModifiedUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@admin.pl",
                            EmailConfirmed = true,
                            GivenName = "admin",
                            IsAdmin = true,
                            Password = "AQAAAAEAACcQAAAAEHKPH+B0FgGZue/91O5Xuy36W/RJWIre4Lk6Tw2XwNVOQKsR4DZgfpG6DTQztryPBw==",
                            PublicId = "14f6cb4b-294e-421e-8054-a8fbb34205dc",
                            Surname = "admin",
                            UserName = "admin@admin.pl"
                        });
                });

            modelBuilder.Entity("Loyalty.System.Data.Model.UserPoints", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CountOfPrize")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("QrCodeToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserPoints", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "14f6cb4b-294e-421e-8054-a8fbb34205dc",
                            CountOfPrize = 0,
                            Points = 0,
                            QrCodeToken = ""
                        });
                });
#pragma warning restore 612, 618
        }
    }
}