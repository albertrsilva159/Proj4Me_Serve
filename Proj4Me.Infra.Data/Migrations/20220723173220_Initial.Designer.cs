﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proj4Me.Infra.Data.Context;

namespace Proj4Me.Infra.Data.Migrations
{
    [DbContext(typeof(ProjetoAreaServicoContext))]
    [Migration("20220723173220_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Proj4Me.Domain.Colaboradores.Colaborador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Colaborador");
                });

            modelBuilder.Entity("Proj4Me.Domain.Perfis.Perfil", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Perfil");
                });

            modelBuilder.Entity("Proj4Me.Domain.ProjetosAreaServicos.ProjetoAreaServico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColaboradorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<Guid?>("PerfilId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Registro")
                        .IsRequired()
                        .HasColumnType("varchar(48)");

                    b.HasKey("Id");

                    b.HasIndex("ColaboradorId");

                    b.HasIndex("PerfilId");

                    b.ToTable("ProjetoAreaServico");
                });

            modelBuilder.Entity("Proj4Me.Domain.ProjetosAreaServicos.ProjetoAreaServico", b =>
                {
                    b.HasOne("Proj4Me.Domain.Colaboradores.Colaborador", "Colaborador")
                        .WithMany("ProjetoAreaServico")
                        .HasForeignKey("ColaboradorId");

                    b.HasOne("Proj4Me.Domain.Perfis.Perfil", "Perfil")
                        .WithMany("ProjetoAreaServico")
                        .HasForeignKey("PerfilId");

                    b.Navigation("Colaborador");

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("Proj4Me.Domain.Colaboradores.Colaborador", b =>
                {
                    b.Navigation("ProjetoAreaServico");
                });

            modelBuilder.Entity("Proj4Me.Domain.Perfis.Perfil", b =>
                {
                    b.Navigation("ProjetoAreaServico");
                });
#pragma warning restore 612, 618
        }
    }
}
