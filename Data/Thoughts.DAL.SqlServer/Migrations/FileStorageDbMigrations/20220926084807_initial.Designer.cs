﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thoughts.DAL;

#nullable disable

namespace Thoughts.DAL.SqlServer.Migrations.FileStorageDbMigrations
{
    [DbContext(typeof(FileStorageDb))]
    [Migration("20220926084807_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Thoughts.DAL.Entities.UploadedFile", b =>
                {
                    b.Property<string>("Sha1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte>("Access")
                        .HasColumnType("tinyint");

                    b.Property<int>("Counter")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FileNameForFileStorage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Flags")
                        .HasColumnType("tinyint");

                    b.Property<string>("Md5")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Meta")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameForDisplay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sha1");

                    b.HasIndex("Md5", "Meta", "Sha1");

                    b.ToTable("Files");
                });
#pragma warning restore 612, 618
        }
    }
}