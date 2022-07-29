﻿// <auto-generated />
using System;
using GoGlobal.Test.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoGlobal.Test.Data.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20220729113631_KeyFix")]
    partial class KeyFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0-preview.6.22329.4");

            modelBuilder.Entity("GoGlobal.Test.Data.Entities.Repository", b =>
                {
                    b.Property<Guid>("RepositoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RepositoryDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RepositoryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RepositoryId");

                    b.ToTable("Repositories");
                });
#pragma warning restore 612, 618
        }
    }
}
