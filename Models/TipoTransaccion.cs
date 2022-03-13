using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppTransferencia.Models
{
	public partial class TipoTransaccion
	{
		public TipoTransaccion()
		{
			Transaccion = new HashSet<Transaccion>();
		}

		public int Id { get; set; }
		[Display(Name = "Tipo Transacción")]
		public string NombreTipoTran { get; set; }

		public virtual ICollection<Transaccion> Transaccion { get; set; }
	}
}
