using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppTransferencia.Models
{
    public partial class Transaccion
    {
        public int Id { get; set; }
        public int? IdTipoTransaccion { get; set; }
        public decimal? ValorGmf { get; set; }
        public int? IdCliente { get; set; }
        public decimal? ValorRetiro { get; set; }
        public DateTime? FechaTransacción { get; set; }

        public virtual Clientes IdClienteNavigation { get; set; }
        public virtual TipoTransaccion IdTipoTransaccionNavigation { get; set; }
    }
}
