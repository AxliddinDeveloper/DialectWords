﻿// <auto-generated />
using System;
using DialectWords.Brokers.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DialectWords.Migrations
{
    [DbContext(typeof(StorageBroker))]
    [Migration("20231220163604_CreateAllTables")]
    partial class CreateAllTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("DialectWords.Models.Foundations.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DialectWords.Models.Foundations.words.Word", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AdabiyTil")
                        .HasColumnType("TEXT");

                    b.Property<string>("Antonim")
                        .HasColumnType("TEXT");

                    b.Property<string>("IngilizTilida")
                        .HasColumnType("TEXT");

                    b.Property<string>("Misollar")
                        .HasColumnType("TEXT");

                    b.Property<string>("Omonim")
                        .HasColumnType("TEXT");

                    b.Property<string>("OzlashganQatlam")
                        .HasColumnType("TEXT");

                    b.Property<string>("RusTilida")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShevaIzohi")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sinonim")
                        .HasColumnType("TEXT");

                    b.Property<string>("Transkripsiya")
                        .HasColumnType("TEXT");

                    b.Property<string>("Transliteratsiya")
                        .HasColumnType("TEXT");

                    b.Property<string>("Turkum")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Words");
                });
#pragma warning restore 612, 618
        }
    }
}
