﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UpdaterServer.Domain;

#nullable disable

namespace UpdaterServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220727125010_Versions")]
    partial class Versions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("UpdaterServer.Domain.Enties.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Author")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentVersion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExeFile")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsWinService")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("UpdaterServer.Domain.Enties.ReleaseAssembly", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("PatchNote")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ReleaseAssemblyId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReleaseAssemblyId");

                    b.ToTable("ReleaseAssemblies");
                });

            modelBuilder.Entity("UpdaterServer.Domain.Enties.ReleaseAssembly", b =>
                {
                    b.HasOne("UpdaterServer.Domain.Enties.Project", "Project")
                        .WithMany("ReleaseAssemblies")
                        .HasForeignKey("ReleaseAssemblyId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("UpdaterServer.Domain.Enties.Project", b =>
                {
                    b.Navigation("ReleaseAssemblies");
                });
#pragma warning restore 612, 618
        }
    }
}