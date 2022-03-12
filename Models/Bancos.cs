using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppTransferencia.Models
{
    public partial class Bancos
    {
        public Bancos()
        {
            Cuentas = new HashSet<Cuentas>();
        }

        public int Id { get; set; }
        public string NombreBanco { get; set; }

        public virtual ICollection<Cuentas> Cuentas { get; set; }
    }
}
