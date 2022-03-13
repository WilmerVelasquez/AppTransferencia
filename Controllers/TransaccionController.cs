using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTransferencia.Data;
using AppTransferencia.Models;

namespace AppTransferencia.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly AppTransferenciaDbContext _context;

        public TransaccionController(AppTransferenciaDbContext context)
        {
            _context = context;
        }

        // GET: Transaccion
        public async Task<IActionResult> Index()
        {
            var appTransferenciaDbContext = _context.Transaccion.Include(t => t.IdClienteNavigation).Include(t => t.IdCuentaDestinoNavigation).Include(t => t.IdCuentaOrigenNavigation).Include(t => t.IdTipoTransaccionNavigation);
            return View(await appTransferenciaDbContext.ToListAsync());
        }

        // GET: Transaccion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccion
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdCuentaDestinoNavigation)
                .Include(t => t.IdCuentaOrigenNavigation)
                .Include(t => t.IdTipoTransaccionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // GET: Crear
        public IActionResult CrearTransaccion()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "NombreCompleto");
            ViewData["IdCuentaDestino"] = new SelectList(_context.Cuentas, "Id", "NumCuenta");
            ViewData["IdCuentaOrigen"] = new SelectList(_context.Cuentas, "Id", "NumCuenta");
            ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccion, "Id", "NombreTipoTran");
            return View();
        }

        // POST: Metodo Crear Nueva Transaccón
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearTransaccion([Bind("Id,IdTipoTransaccion,ValorGmf,IdCliente,ValorRetiro,IdCuentaOrigen,IdCuentaDestino,FechaTransacción")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Apellidos", transaccion.IdCliente);
            ViewData["IdCuentaDestino"] = new SelectList(_context.Cuentas, "Id", "CuentaSaldo", transaccion.IdCuentaDestino);
            ViewData["IdCuentaOrigen"] = new SelectList(_context.Cuentas, "Id", "NumCuenta", transaccion.IdCuentaOrigen);           
            ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccion, "Id", "Id", transaccion.IdTipoTransaccion);
            return View(transaccion);
        }

        
    }
}
