﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TIPIESProj.DataBase;

namespace TIPIESProj.DataBase.Migrations
{
    [DbContext(typeof(ChartDB))]
    [Migration("20211115194911_changed_relationsips")]
    partial class changed_relationsips
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TIPIESProj.DataBase.Models.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("TIPIESProj.DataBase.Models.ChartOfAccounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("AccountNumber")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SudKonto1")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ChartOfAccounts");
                });

            modelBuilder.Entity("TIPIESProj.DataBase.Models.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ChartOfAccountsId")
                        .HasColumnType("integer");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("TIPIESProj.DataBase.Models.OperationLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BuyerId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DivisionId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("ProductId");

                    b.ToTable("OperationLogs");
                });

            modelBuilder.Entity("TIPIESProj.DataBase.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PlannedCostPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TIPIESProj.DataBase.Models.TransactionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int?>("CreditId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DebetId")
                        .HasColumnType("integer");

                    b.Property<int>("OperationLogId")
                        .HasColumnType("integer");

                    b.Property<string>("SubKontoK1")
                        .HasColumnType("text");

                    b.Property<string>("SudKontoD1")
                        .HasColumnType("text");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<int?>("TransactionLogId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreditId");

                    b.HasIndex("DebetId");

                    b.HasIndex("TransactionLogId");

                    b.ToTable("TransactionLogs");
                });

            modelBuilder.Entity("TIPIESProj.DataBase.Models.Division", b =>
                {
                    b.HasOne("TIPIESProj.DataBase.Models.ChartOfAccounts", "ChartOfAccounts")
                        .WithMany("Divisions")
                        .HasForeignKey("DivisionId");
                });

            modelBuilder.Entity("TIPIESProj.DataBase.Models.OperationLog", b =>
                {
                    b.HasOne("TIPIESProj.DataBase.Models.Buyer", "Buyer")
                        .WithMany("OperationLogs")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TIPIESProj.DataBase.Models.Division", "Division")
                        .WithMany("OperationLogs")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TIPIESProj.DataBase.Models.Product", "Product")
                        .WithMany("OperationLogs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TIPIESProj.DataBase.Models.TransactionLog", b =>
                {
                    b.HasOne("TIPIESProj.DataBase.Models.ChartOfAccounts", "Credit")
                        .WithMany("Credits")
                        .HasForeignKey("CreditId");

                    b.HasOne("TIPIESProj.DataBase.Models.ChartOfAccounts", "Debet")
                        .WithMany("Debets")
                        .HasForeignKey("DebetId");

                    b.HasOne("TIPIESProj.DataBase.Models.OperationLog", "OperationLog")
                        .WithMany("TransactionLog")
                        .HasForeignKey("TransactionLogId");
                });
#pragma warning restore 612, 618
        }
    }
}
