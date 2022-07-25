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
    [Migration("20220724065049_ExeFile")]
    partial class ExeFile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("UpdaterServer.Domain.Enties.App", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.HasKey("Id");

                    b.ToTable("Apps");
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

                    b.HasKey("Id");

                    b.HasIndex("ReleaseAssemblyId");

                    b.ToTable("ReleaseAssemblies");
                });

            modelBuilder.Entity("UpdaterServer.Domain.Enties.ReleaseAssembly", b =>
                {
                    b.HasOne("UpdaterServer.Domain.Enties.App", "App")
                        .WithMany("ReleaseAssemblies")
                        .HasForeignKey("ReleaseAssemblyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("UpdaterServer.Domain.Enties.App", b =>
                {
                    b.Navigation("ReleaseAssemblies");
                });
#pragma warning restore 612, 618
        }
    }
}
