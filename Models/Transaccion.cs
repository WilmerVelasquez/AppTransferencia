using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppTransferencia.Models
{
	public partial class Transaccion
	{
		public int Id { get; set; }
		[Display(Name ="Tipo Transacción")]
		public int IdTipoTransaccion { get; set; }
		[DataType(DataType.Currency)]
		[Column(TypeName = "money")]
		[Display(Name ="Valor GMF")]
		public decimal? ValorGmf { get; set; }
		public int? IdCliente { get; set; }
		[Display(Name ="Valor Retiro")]
		public decimal? ValorRetiro { get; set; }
		[Display(Name = "Cuenta Origen")]
		public int IdCuentaOrigen { get; set; }
		[Display(Name = "Cuenta Destino")]
		public int? IdCuentaDestino { get; set; }
		[Display(Name = " Fecha Transacción")]
		public DateTime? FechaTransacción { get; set; }


		[Display(Name ="Nombres")]
		public virtual Clientes IdClienteNavigation { get; set; }
		[Display(Name ="Cuenta Destino")]
		public virtual Cuentas IdCuentaDestinoNavigation { get; set; }
		[Display(Name ="Cuenta Origen")]
		public virtual Cuentas IdCuentaOrigenNavigation { get; set; }
		[Display(Name ="Tipo Transacción")]
		public virtual TipoTransaccion IdTipoTransaccionNavigation { get; set; }
	}
}
