﻿// <auto-generated />
using System;
using Bmerketo.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bmerketo.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bmerketo.Models.Entities.BrandEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BrandEntity");
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryEntity");
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.ColorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ColorEntity");
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.ProductEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandEntityId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryEntityId")
                        .HasColumnType("int");

                    b.Property<string>("ImageAlt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandEntityId");

                    b.HasIndex("CategoryEntityId");

                    b.ToTable("ProductEntity");
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.Product_ColorEntity", b =>
                {
                    b.Property<int>("ColorEntityId")
                        .HasColumnType("int");

                    b.Property<int>("ProductEntityId")
                        .HasColumnType("int");

                    b.HasKey("ColorEntityId", "ProductEntityId");

                    b.HasIndex("ProductEntityId");

                    b.ToTable("Product_ColorEntity");
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.ReviewEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductEntityId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductEntityId");

                    b.ToTable("ReviewEntity");
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.ProductEntity", b =>
                {
                    b.HasOne("Bmerketo.Models.Entities.BrandEntity", "BrandEntity")
                        .WithMany()
                        .HasForeignKey("BrandEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bmerketo.Models.Entities.CategoryEntity", "CategoryEntity")
                        .WithMany()
                        .HasForeignKey("CategoryEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BrandEntity");

                    b.Navigation("CategoryEntity");
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.Product_ColorEntity", b =>
                {
                    b.HasOne("Bmerketo.Models.Entities.ColorEntity", "ColorEntity")
                        .WithMany("Product_ColorEntity")
                        .HasForeignKey("ColorEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bmerketo.Models.Entities.ProductEntity", "ProductEntity")
                        .WithMany("Product_ColorEntity")
                        .HasForeignKey("ProductEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ColorEntity");

                    b.Navigation("ProductEntity");
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.ReviewEntity", b =>
                {
                    b.HasOne("Bmerketo.Models.Entities.ProductEntity", null)
                        .WithMany("ReviewEntity")
                        .HasForeignKey("ProductEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.ColorEntity", b =>
                {
                    b.Navigation("Product_ColorEntity");
                });

            modelBuilder.Entity("Bmerketo.Models.Entities.ProductEntity", b =>
                {
                    b.Navigation("Product_ColorEntity");

                    b.Navigation("ReviewEntity");
                });
#pragma warning restore 612, 618
        }
    }
}