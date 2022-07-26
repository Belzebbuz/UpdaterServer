﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UpdaterServer.Domain;

#nullable disable

namespace UpdaterServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("IdentityRole");

                    b.HasData(
                        new
                        {
                            Id = "3623d53c-c317-40a1-8c45-178bd8e12677",
                            ConcurrencyStamp = "64014581-7d47-40f0-ba6f-66a9bd254c02",
                            Name = "Viewer",
                            NormalizedName = "VIEWER"
                        },
                        new
                        {
                            Id = "2ea7bad5-b551-4cc8-a440-d76398ceeb18",
                            ConcurrencyStamp = "13146fa8-acef-4d07-aa38-04457583c02a",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "70f0e057-bbc9-44ec-bf3b-32043d6b2f80",
                            ConcurrencyStamp = "676590ba-f18c-4571-9479-bd31562f1e5e",
                            Name = "Dev",
                            NormalizedName = "DEV"
                        });
                });

            modelBuilder.Entity("UpdaterServer.Domain.Enties.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExeFile")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsWinService")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .IsRequired()
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
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ReleaseAssemblyId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReleaseAssemblyId");

                    b.ToTable("ReleaseAssemblies");
                });

            modelBuilder.Entity("UpdaterServer.Domain.Enties.ReleaseAssembly", b =>
                {
                    b.HasOne("UpdaterServer.Domain.Enties.Project", "Project")
                        .WithMany("ReleaseAssemblies")
                        .HasForeignKey("ReleaseAssemblyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
