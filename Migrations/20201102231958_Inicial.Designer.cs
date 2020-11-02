﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jean_P2_AP1.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20201102231958_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("Jean_P2_AP1.Entidades.Proyectos", b =>
                {
                    b.Property<int>("ProyectoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<double>("TiempoTotal")
                        .HasColumnType("REAL");

                    b.HasKey("ProyectoId");

                    b.ToTable("Proyectos");
                });

            modelBuilder.Entity("Jean_P2_AP1.Entidades.ProyectosDetalle", b =>
                {
                    b.Property<int>("ProyectosDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProyectoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Requerimiento")
                        .HasColumnType("TEXT");

                    b.Property<double>("Tiempo")
                        .HasColumnType("REAL");

                    b.Property<int>("TipoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProyectosDetalleId");

                    b.HasIndex("ProyectoId");

                    b.HasIndex("TipoId");

                    b.ToTable("ProyectosDetalle");
                });

            modelBuilder.Entity("Jean_P2_AP1.Entidades.TiposTarea", b =>
                {
                    b.Property<int>("TipoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.HasKey("TipoId");

                    b.ToTable("TiposTarea");

                    b.HasData(
                        new
                        {
                            TipoId = 1,
                            Descripcion = "Análisis"
                        },
                        new
                        {
                            TipoId = 2,
                            Descripcion = "Diseño"
                        },
                        new
                        {
                            TipoId = 3,
                            Descripcion = "Programación"
                        });
                });

            modelBuilder.Entity("Jean_P2_AP1.Entidades.ProyectosDetalle", b =>
                {
                    b.HasOne("Jean_P2_AP1.Entidades.Proyectos", null)
                        .WithMany("Detalle")
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jean_P2_AP1.Entidades.TiposTarea", null)
                        .WithMany("Detalle")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}