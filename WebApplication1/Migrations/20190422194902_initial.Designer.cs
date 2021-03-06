﻿// <auto-generated />
using System;
using EmployeeService.infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

namespace EmployeeService.Migrations
{
    [DbContext(typeof(InventoryContext))]
    [Migration("20190422194902_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("EmployeeService.Models.CDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Lb_no");

                    b.Property<string>("make");

                    b.Property<string>("sl_no");

                    b.HasKey("Id");

                    b.ToTable("CDetails");
                });

            modelBuilder.Entity("EmployeeService.Models.CLoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CDetailId");

                    b.Property<int?>("serNo");

                    b.HasKey("Id");

                    b.HasIndex("CDetailId");

                    b.ToTable("CLoans");
                });

            modelBuilder.Entity("EmployeeService.Models.CLoan", b =>
                {
                    b.HasOne("EmployeeService.Models.CDetail")
                        .WithMany("CLoan")
                        .HasForeignKey("CDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
