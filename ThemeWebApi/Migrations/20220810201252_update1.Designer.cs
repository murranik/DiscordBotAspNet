﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThemeWebApi.Database;

#nullable disable

namespace ThemeWebApi.Migrations
{
    [DbContext(typeof(ThemeDbContext))]
    [Migration("20220810201252_update1")]
    partial class update1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ThemeWebApi.Database.Models.DataTableCellColors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DefaultBorderColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultBoxShadowColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultEditColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultInputTextColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ParentName")
                        .IsUnique()
                        .HasFilter("[ParentName] IS NOT NULL");

                    b.ToTable("DataTableCellColors");
                });

            modelBuilder.Entity("ThemeWebApi.Database.Models.DropdownButtonColors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DefaultBarrierColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultIconDisabledColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultIconEnableColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ParentName")
                        .IsUnique()
                        .HasFilter("[ParentName] IS NOT NULL");

                    b.ToTable("DropdownButtonColors");
                });

            modelBuilder.Entity("ThemeWebApi.Database.Models.FloatingBoxColors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DefaultBackgroundColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultShadowColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ParentName")
                        .IsUnique()
                        .HasFilter("[ParentName] IS NOT NULL");

                    b.ToTable("FloatingBoxColors");
                });

            modelBuilder.Entity("ThemeWebApi.Database.Models.ThemeData", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActiveColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CancelColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultAppBackGroundColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultNavMenuBackgroundColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultNavMenuTextColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultTextColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EditColor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("ThemeWebApi.Database.Models.DataTableCellColors", b =>
                {
                    b.HasOne("ThemeWebApi.Database.Models.ThemeData", null)
                        .WithOne("DataTableCellColors")
                        .HasForeignKey("ThemeWebApi.Database.Models.DataTableCellColors", "ParentName");
                });

            modelBuilder.Entity("ThemeWebApi.Database.Models.DropdownButtonColors", b =>
                {
                    b.HasOne("ThemeWebApi.Database.Models.ThemeData", null)
                        .WithOne("DropdownButtonColors")
                        .HasForeignKey("ThemeWebApi.Database.Models.DropdownButtonColors", "ParentName");
                });

            modelBuilder.Entity("ThemeWebApi.Database.Models.FloatingBoxColors", b =>
                {
                    b.HasOne("ThemeWebApi.Database.Models.ThemeData", null)
                        .WithOne("FloatingBoxColors")
                        .HasForeignKey("ThemeWebApi.Database.Models.FloatingBoxColors", "ParentName");
                });

            modelBuilder.Entity("ThemeWebApi.Database.Models.ThemeData", b =>
                {
                    b.Navigation("DataTableCellColors");

                    b.Navigation("DropdownButtonColors");

                    b.Navigation("FloatingBoxColors");
                });
#pragma warning restore 612, 618
        }
    }
}