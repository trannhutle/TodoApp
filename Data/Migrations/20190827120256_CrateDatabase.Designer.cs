﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApplication.Data;

namespace TodoApplication.Data.Migrations
{
    [DbContext(typeof(TodoApplicationContext))]
    [Migration("20190827120256_CrateDatabase")]
    partial class CrateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("TodoApplication.Models.Todo", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AssignmentDate");

                    b.Property<int>("CatID");

                    b.Property<string>("Content");

                    b.Property<long>("CreateDate")
                        .HasColumnName("CreateDate");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TodoApplication.Models.TodoCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("TodoCategory");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Work"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Home"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Personal"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}