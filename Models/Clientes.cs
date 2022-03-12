using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppTransferencia.Models
{
    public partial class Clientes
    {
        public Clientes()
        {
            Cuentas = new HashSet<Cuentas>();
            Transaccion = new HashSet<Transaccion>();
        }

        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool Gmf { get; set; }

        public virtual ICollection<Cuentas> Cuentas { get; set; }
        public virtual ICollection<Transaccion> Transaccion { get; set; }
    }
}
