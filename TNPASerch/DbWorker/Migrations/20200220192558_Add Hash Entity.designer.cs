﻿// <auto-generated />
using System;
using DbWorker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DbWorker.Migrations
{
    [DbContext(typeof(TnpaDbContext))]
    [Migration("20200220192558_Add Hash Entity")]
    partial class AddHashEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("DAL.Change", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PutIntoOperation")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Registered")
                        .HasColumnType("TEXT");

                    b.Property<int>("TnpaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TnpaId");

                    b.ToTable("Change");
                });

            modelBuilder.Entity("DAL.DataFileInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HashCode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<int>("TnpaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TnpaId");

                    b.ToTable("DataFileInfo");
                });

            modelBuilder.Entity("DAL.FolderHashCod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("FolderHashCods");
                });

            modelBuilder.Entity("DAL.Tnpa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Cancelled")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsReal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberRegistered")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PutIntoOperation")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Registered")
                        .HasColumnType("TEXT");

                    b.Property<int>("TnpaTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TnpaTypeId");

                    b.ToTable("Tnpas");
                });

            modelBuilder.Entity("DAL.TnpaType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TnpaTypes");
                });

            modelBuilder.Entity("DAL.Change", b =>
                {
                    b.HasOne("DAL.Tnpa", "Tnpa")
                        .WithMany("Changes")
                        .HasForeignKey("TnpaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.DataFileInfo", b =>
                {
                    b.HasOne("DAL.Tnpa", "Tnpa")
                        .WithMany("Files")
                        .HasForeignKey("TnpaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Tnpa", b =>
                {
                    b.HasOne("DAL.TnpaType", "Type")
                        .WithMany("Tnpas")
                        .HasForeignKey("TnpaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}