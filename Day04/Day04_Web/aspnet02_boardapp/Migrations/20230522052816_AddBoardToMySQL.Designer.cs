﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aspnet02_boardapp.Data;

#nullable disable

namespace aspnet02_boardapp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230522052816_AddBoardToMySQL")]
    partial class AddBoardToMySQL
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("aspnet02_boardapp.Models.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contents")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReadCount")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });
#pragma warning restore 612, 618
        }
    }
}
