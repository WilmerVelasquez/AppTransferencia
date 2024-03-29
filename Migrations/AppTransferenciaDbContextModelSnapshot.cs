﻿// <auto-generated />
using System;
using AppTransferencia.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppTransferencia.Migrations
{
	[DbContext(typeof(AppTransferenciaDbContext))]
    partial class AppTransferenciaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppTransferencia.Models.Bancos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NombreBanco")
                        .HasColumnName("Nombre_Banco")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Bancos");
                });

            modelBuilder.Entity("AppTransferencia.Models.Clientes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<bool>("Gmf")
                        .HasColumnName("GMF")
                        .HasColumnType("bit");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("varchar(14)")
                        .HasMaxLength(14)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("AppTransferencia.Models.Cuentas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdBanco")
                        .HasColumnName("Id_Banco")
                        .HasColumnType("int");

                    b.Property<int>("IdCliente")
                        .HasColumnName("Id_Cliente")
                        .HasColumnType("int");

                    b.Property<string>("NumCuenta")
                        .IsRequired()
                        .HasColumnName("Num_Cuenta")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<decimal>("Saldo")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("IdBanco");

                    b.HasIndex("IdCliente");

                    b.ToTable("Cuentas");
                });

            modelBuilder.Entity("AppTransferencia.Models.TipoTransaccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NombreTipoTran")
                        .HasColumnName("Nombre_Tipo_Tran")
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("TipoTransaccion");
                });

            modelBuilder.Entity("AppTransferencia.Models.Transaccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CuentaDestinoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaTransacción")
                        .HasColumnName("Fecha_Transacción")
                        .HasColumnType("datetime");

                    b.Property<int>("IdCliente")
                        .HasColumnName("Id_Cliente")
                        .HasColumnType("int");

                    b.Property<int>("IdCuentaDestino")
                        .HasColumnName("Cuenta_Destino")
                        .HasColumnType("int");

                    b.Property<int>("IdCuentaOrigen")
                        .HasColumnName("Cuenta_Origen")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoTransaccion")
                        .HasColumnName("Id_Tipo_Transaccion")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorGmf")
                        .HasColumnName("Valor_GMF")
                        .HasColumnType("money");

                    b.Property<decimal>("ValorRetiro")
                        .HasColumnName("Valor_Retiro")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CuentaDestinoId");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdCuentaOrigen");

                    b.HasIndex("IdTipoTransaccion");

                    b.ToTable("Transaccion");
                });

            modelBuilder.Entity("AppTransferencia.Models.Cuentas", b =>
                {
                    b.HasOne("AppTransferencia.Models.Bancos", "Banco")
                        .WithMany("Cuentas")
                        .HasForeignKey("IdBanco")
                        .HasConstraintName("FK_Cuentas_Bancos")
                        .IsRequired();

                    b.HasOne("AppTransferencia.Models.Clientes", "Cliente")
                        .WithMany("Cuentas")
                        .HasForeignKey("IdCliente")
                        .HasConstraintName("FK_Cuentas_Clientes")
                        .IsRequired();
                });

            modelBuilder.Entity("AppTransferencia.Models.Transaccion", b =>
                {
                    b.HasOne("AppTransferencia.Models.Cuentas", "CuentaDestino")
                        .WithMany()
                        .HasForeignKey("CuentaDestinoId");

                    b.HasOne("AppTransferencia.Models.Clientes", "Cliente")
                        .WithMany("Transaccion")
                        .HasForeignKey("IdCliente")
                        .HasConstraintName("FK_Transaccion_Clientes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppTransferencia.Models.Cuentas", "CuentaOrigen")
                        .WithMany("Transaccion")
                        .HasForeignKey("IdCuentaOrigen")
                        .HasConstraintName("FK_Transaccion_Cuentas1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppTransferencia.Models.TipoTransaccion", "TipoTransaccion")
                        .WithMany("Transaccion")
                        .HasForeignKey("IdTipoTransaccion")
                        .HasConstraintName("FK_Transaccion_TipoTransaccion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
