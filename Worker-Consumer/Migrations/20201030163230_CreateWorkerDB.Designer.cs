﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Worker_Consumer.Models;

namespace Worker_Consumer.Migrations
{
    [DbContext(typeof(PersonDBContext))]
    [Migration("20201030163230_CreateWorkerDB")]
    partial class CreateWorkerDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Worker_Consumer.Models.Cotista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodFundo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComunicEletr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonID");

                    b.ToTable("Cotista");
                });

            modelBuilder.Entity("Worker_Consumer.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("agencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("conta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Worker_Consumer.Models.Cotista", b =>
                {
                    b.HasOne("Worker_Consumer.Models.Person", null)
                        .WithMany("Cotista")
                        .HasForeignKey("PersonID");
                });
#pragma warning restore 612, 618
        }
    }
}