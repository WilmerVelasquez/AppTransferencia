using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppTransferencia.Models
{
	public partial class Cuentas
	{
		public int Id { get; set; }
		public int IdBanco { get; set; }
		[DataType(DataType.Currency)]
		[Column(TypeName = "money")]
		public decimal Saldo { get; set; }
		public int IdCliente { get; set; }
		[Display(Name ="Número Cuenta")]
		public string NumCuenta { get; set; }

		public virtual Bancos Banco { get; set; }
		public virtual Clientes Cliente { get; set; }
	}
}
