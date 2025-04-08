﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsAPI.Models;

#nullable disable

namespace ProductsAPI.Migrations
{
    [DbContext(typeof(ProductsContext))]
    partial class ProductsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("ProductsAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            IsActive = true,
                            Price = 100m,
                            ProductName = "Product 1"
                        },
                        new
                        {
                            ProductId = 2,
                            IsActive = true,
                            Price = 200m,
                            ProductName = "Product 2"
                        },
                        new
                        {
                            ProductId = 3,
                            IsActive = true,
                            Price = 300m,
                            ProductName = "Product 3"
                        },
                        new
                        {
                            ProductId = 4,
                            IsActive = true,
                            Price = 400m,
                            ProductName = "Product 4"
                        },
                        new
                        {
                            ProductId = 5,
                            IsActive = true,
                            Price = 500m,
                            ProductName = "Product 5"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
