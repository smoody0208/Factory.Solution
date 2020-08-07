﻿// <auto-generated />
using System;
using Factory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Factory.Migrations
{
    [DbContext(typeof(FactoryContext))]
    [Migration("20200807221603_ChangeStatusToBool")]
    partial class ChangeStatusToBool
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Factory.Models.Engineer", b =>
                {
                    b.Property<int>("EngineerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("EngineerId");

                    b.ToTable("Engineers");
                });

            modelBuilder.Entity("Factory.Models.EngineerLocationMachine", b =>
                {
                    b.Property<int>("EngineerLocationMachineId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EngineerId");

                    b.Property<int?>("LocationId");

                    b.Property<int?>("MachineId");

                    b.HasKey("EngineerLocationMachineId");

                    b.HasIndex("EngineerId");

                    b.HasIndex("LocationId");

                    b.HasIndex("MachineId");

                    b.ToTable("EngineerLocationMachine");
                });

            modelBuilder.Entity("Factory.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Factory.Models.Machine", b =>
                {
                    b.Property<int>("MachineId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.HasKey("MachineId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Factory.Models.EngineerLocationMachine", b =>
                {
                    b.HasOne("Factory.Models.Engineer", "Engineer")
                        .WithMany("LocationsMachines")
                        .HasForeignKey("EngineerId");

                    b.HasOne("Factory.Models.Location", "Location")
                        .WithMany("EngineersMachines")
                        .HasForeignKey("LocationId");

                    b.HasOne("Factory.Models.Machine", "Machine")
                        .WithMany("EngineersLocations")
                        .HasForeignKey("MachineId");
                });
#pragma warning restore 612, 618
        }
    }
}
