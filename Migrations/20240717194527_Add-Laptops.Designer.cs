﻿// <auto-generated />
using System;
using MediaStore_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediaStore_backend.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20240717194527_Add-Laptops")]
    partial class AddLaptops
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("MediaStore_backend.Models.BigPromoItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PriceLabel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SloganLabel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TopLabel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("theme")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("BigPromoItems", (string)null);
                });

            modelBuilder.Entity("MediaStore_backend.Models.Categories.Laptop", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Battery")
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<string>("Display")
                        .HasColumnType("TEXT");

                    b.Property<string>("Features")
                        .HasColumnType("TEXT");

                    b.Property<string>("Material")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ports")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resolution")
                        .HasColumnType("TEXT");

                    b.Property<float?>("ScreenSize")
                        .HasColumnType("REAL");

                    b.Property<string>("StorageSize")
                        .HasColumnType("TEXT");

                    b.Property<string>("StorageType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Weight")
                        .HasColumnType("TEXT");

                    b.Property<int>("category")
                        .HasColumnType("INTEGER");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("oldprice")
                        .HasColumnType("INTEGER");

                    b.Property<float>("price")
                        .HasColumnType("REAL");

                    b.Property<int?>("rating")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("reviews")
                        .HasColumnType("INTEGER");

                    b.Property<string>("specialTags")
                        .HasColumnType("TEXT");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Laptops", (string)null);
                });

            modelBuilder.Entity("MediaStore_backend.Models.Categories.Smartphone", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Battery")
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<string>("Camera")
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<string>("Connectivity")
                        .HasColumnType("TEXT");

                    b.Property<string>("Display")
                        .HasColumnType("TEXT");

                    b.Property<string>("Features")
                        .HasColumnType("TEXT");

                    b.Property<string>("Material")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resolution")
                        .HasColumnType("TEXT");

                    b.Property<float?>("ScreenSize")
                        .HasColumnType("REAL");

                    b.Property<string>("Storage")
                        .HasColumnType("TEXT");

                    b.Property<string>("Weight")
                        .HasColumnType("TEXT");

                    b.Property<int>("category")
                        .HasColumnType("INTEGER");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("oldprice")
                        .HasColumnType("INTEGER");

                    b.Property<float>("price")
                        .HasColumnType("REAL");

                    b.Property<int?>("rating")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("reviews")
                        .HasColumnType("INTEGER");

                    b.Property<string>("specialTags")
                        .HasColumnType("TEXT");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Smartphones", (string)null);
                });

            modelBuilder.Entity("MediaStore_backend.Models.NewsCarouselItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("NewsCarouselItems", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
