﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPeople.Services.Posts.Infrastructure.Data;

#nullable disable

namespace MyPeople.Services.Posts.Infrastructure.Migrations.SqlServer.Application
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyPeople.Services.Posts.Domain.Entities.Post", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("MyPeople.Services.Posts.Domain.Entities.PostImage", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostImages");
                });

            modelBuilder.Entity("MyPeople.Services.Posts.Domain.Entities.PostImage", b =>
                {
                    b.HasOne("MyPeople.Services.Posts.Domain.Entities.Post", "Post")
                        .WithMany("ImagesIds")
                        .HasForeignKey("PostId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("MyPeople.Services.Posts.Domain.Entities.Post", b =>
                {
                    b.Navigation("ImagesIds");
                });
#pragma warning restore 612, 618
        }
    }
}
