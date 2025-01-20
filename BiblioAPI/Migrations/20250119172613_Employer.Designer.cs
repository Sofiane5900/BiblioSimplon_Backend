﻿// <auto-generated />
using System;
using BiblioAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiblioAPI.Migrations
{
    [DbContext(typeof(BiblioDbContext))]
    [Migration("20250119172613_Employer")]
    partial class Employer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("BiblioAPI.Models.EmployeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Employe", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@biblio.com",
                            Password = "admin123",
                            Role = "Admin",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "biblio@tecaire.com",
                            Password = "biblio123",
                            Role = "Bibliothecaire",
                            Username = "biblio"
                        });
                });

            modelBuilder.Entity("BiblioAPI.Models.EmpruntModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateEmprunt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateRetour")
                        .HasColumnType("TEXT");

                    b.Property<int>("LivreId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MembreId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LivreId");

                    b.HasIndex("MembreId");

                    b.ToTable("Emprunt", (string)null);
                });

            modelBuilder.Entity("BiblioAPI.Models.LivreModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Auteur")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("EstDisponible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ISBN")
                        .IsUnique();

                    b.ToTable("Livre", (string)null);
                });

            modelBuilder.Entity("BiblioAPI.Models.MembreModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Membre", (string)null);
                });

            modelBuilder.Entity("BiblioAPI.Models.EmpruntModel", b =>
                {
                    b.HasOne("BiblioAPI.Models.LivreModel", "Livre")
                        .WithMany("Emprunts")
                        .HasForeignKey("LivreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiblioAPI.Models.MembreModel", "Membre")
                        .WithMany("Emprunts")
                        .HasForeignKey("MembreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Livre");

                    b.Navigation("Membre");
                });

            modelBuilder.Entity("BiblioAPI.Models.LivreModel", b =>
                {
                    b.Navigation("Emprunts");
                });

            modelBuilder.Entity("BiblioAPI.Models.MembreModel", b =>
                {
                    b.Navigation("Emprunts");
                });
#pragma warning restore 612, 618
        }
    }
}
