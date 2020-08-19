﻿// <auto-generated />
using System;
using BrixBank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BrixBank.Data.Migrations
{
    [DbContext(typeof(BrixBankContext))]
    [Migration("20200818060020_SixMigration")]
    partial class SixMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BrixBank.Data.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BrixBank.Data.Entities.LoanRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoanRequestrId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoanSupplied")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LoanRequests");
                });

            modelBuilder.Entity("BrixBank.Data.Entities.Rules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId2CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Kind")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Law")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Operator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Output")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId2CustomerId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("BrixBank.Data.Entities.Rules", b =>
                {
                    b.HasOne("BrixBank.Data.Entities.Customer", "CustomerId2")
                        .WithMany()
                        .HasForeignKey("CustomerId2CustomerId");
                });
#pragma warning restore 612, 618
        }
    }
}
