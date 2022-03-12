using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppTransferencia.Models
{
    public partial class Cuentas
    {
        public int Id { get; set; }
        public int IdBanco { get; set; }
        public decimal Saldo { get; set; }
        public int IdCliente { get; set; }
        public string NumCuenta { get; set; }

        public virtual Bancos IdBancoNavigation { get; set; }
        public virtual Clientes IdClienteNavigation { get; set; }
    }
}
