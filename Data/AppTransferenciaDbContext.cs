using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AppTransferencia.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppTransferencia.Data
{
	public partial class AppTransferenciaDbContext : DbContext
	{
		public AppTransferenciaDbContext()
		{
		}

		public AppTransferenciaDbContext(DbContextOptions<AppTransferenciaDbContext> options)
		    : base(options)
		{
		}

		public virtual DbSet<Bancos> Bancos { get; set; }
		public virtual DbSet<Clientes> Clientes { get; set; }
		public virtual DbSet<Cuentas> Cuentas { get; set; }
		public virtual DbSet<GravamenFinanciero> GravamenFinanciero { get; set; }
		public virtual DbSet<TipoTransaccion> TipoTransaccion { get; set; }
		public virtual DbSet<Transaccion> Transaccion { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
//			if (!optionsBuilder.IsConfigured)
//			{
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//				optionsBuilder.UseSqlServer("server=localhost\\MSSQLSERVER01;Database=BDAppBancaria;Trusted_Connection=True;");
//			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Bancos>(entity =>
			{
				entity.Property(e => e.NombreBanco)
			    .HasColumnName("Nombre_Banco")
			    .HasMaxLength(30)
			    .IsUnicode(false);
			});

			modelBuilder.Entity<Clientes>(entity =>
			{
				entity.Property(e => e.Apellidos)
			    .IsRequired()
			    .HasMaxLength(20)
			    .IsUnicode(false);

				entity.Property(e => e.Gmf).HasColumnName("GMF");

				entity.Property(e => e.Nombres)
			    .IsRequired()
			    .HasMaxLength(14)
			    .IsUnicode(false);
			});

			modelBuilder.Entity<Cuentas>(entity =>
			{
				entity.Property(e => e.IdBanco).HasColumnName("Id_Banco");

				entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");

				entity.Property(e => e.NumCuenta)
			    .IsRequired()
			    .HasColumnName("Num_Cuenta")
			    .HasMaxLength(20)
			    .IsUnicode(false);

				entity.Property(e => e.Saldo).HasColumnType("money");

				entity.HasOne(d => d.IdBancoNavigation)
			    .WithMany(p => p.Cuentas)
			    .HasForeignKey(d => d.IdBanco)
			    .OnDelete(DeleteBehavior.ClientSetNull)
			    .HasConstraintName("FK_Cuentas_Bancos");

				entity.HasOne(d => d.IdClienteNavigation)
			    .WithMany(p => p.Cuentas)
			    .HasForeignKey(d => d.IdCliente)
			    .OnDelete(DeleteBehavior.ClientSetNull)
			    .HasConstraintName("FK_Cuentas_Clientes");
			});

			modelBuilder.Entity<GravamenFinanciero>(entity =>
			{
				entity.Property(e => e.NombreGravamen)
			    .HasColumnName("Nombre_Gravamen")
			    .HasMaxLength(20)
			    .IsUnicode(false);
			});

			modelBuilder.Entity<TipoTransaccion>(entity =>
			{
				entity.Property(e => e.NombreTipoTran)
			    .HasColumnName("Nombre_Tipo_Tran")
			    .HasMaxLength(25)
			    .IsUnicode(false);
			});

			modelBuilder.Entity<Transaccion>(entity =>
			{
				entity.Property(e => e.FechaTransacción)
			    .HasColumnName("Fecha_Transacción")
			    .HasColumnType("datetime");

				entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");

				entity.Property(e => e.IdTipoTransaccion).HasColumnName("Id_Tipo_Transaccion");

				entity.Property(e => e.ValorGmf)
			    .HasColumnName("Valor_GMF")
			    .HasColumnType("money");

				entity.Property(e => e.ValorRetiro)
			    .HasColumnName("Valor_Retiro")
			    .HasColumnType("money");

				entity.HasOne(d => d.IdClienteNavigation)
			    .WithMany(p => p.Transaccion)
			    .HasForeignKey(d => d.IdCliente)
			    .HasConstraintName("FK_Transaccion_Clientes");

				entity.HasOne(d => d.IdCuentaDestinoNavigation)
			    .WithMany(p => p.TransaccionIdCuentaDestinoNavigation)
			    .HasForeignKey(d => d.IdCuentaDestino)
			    .HasConstraintName("FK_Transaccion_Cuentas1");

				entity.HasOne(d => d.IdCuentaOrigenNavigation)
			    .WithMany(p => p.TransaccionIdCuentaOrigenNavigation)
			    .HasForeignKey(d => d.IdCuentaOrigen)
			    .HasConstraintName("FK_Transaccion_Cuentas");

				entity.HasOne(d => d.IdTipoTransaccionNavigation)
			    .WithMany(p => p.Transaccion)
			    .HasForeignKey(d => d.IdTipoTransaccion)
			    .HasConstraintName("FK_Transaccion_TipoTransaccion");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
