using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTransferencia.Data;
using AppTransferencia.Models;
using Microsoft.AspNetCore.Authorization;

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

		// GET:Metodo para visualizar la lista de registros y su detalle 
		//public async Task<IActionResult> Detalle(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var transaccion = await _context.Transaccion
		//        .Include(t => t.IdClienteNavigation)
		//        .Include(t => t.IdCuentaDestinoNavigation)
		//        .Include(t => t.IdCuentaOrigenNavigation)
		//        .Include(t => t.IdTipoTransaccionNavigation)
		//        .FirstOrDefaultAsync(m => m.Id == id);
		//    if (transaccion == null)
		//    {
		//        return NotFound();
		//    }

		//    return View(transaccion);
		//}

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
				Cuentas cuenta = new Cuentas();
				TipoTransaccion tipoTransa = _context.TipoTransaccion.Where(c => c.Id == transaccion.IdTipoTransaccion).FirstOrDefault();

				if (tipoTransa.NombreTipoTran == "Transferencia")
				{
					cuenta = _context.Cuentas.Where(c => c.Id == transaccion.IdCuentaDestino).FirstOrDefault();
					cuenta.Saldo = cuenta.Saldo + transaccion.ValorRetiro.GetValueOrDefault();
					_context.Add(cuenta);
					await _context.SaveChangesAsync();
				}

				cuenta = _context.Cuentas.Where(c => c.Id == transaccion.IdCuentaOrigen).FirstOrDefault();
				cuenta.Saldo = cuenta.Saldo - transaccion.ValorRetiro.GetValueOrDefault();
				_context.Add(cuenta);

				Clientes c = _context.Clientes.Where(c => c.Id == transaccion.IdCliente).FirstOrDefault();
				if (c.Gmf)
				{
					transaccion.ValorGmf = transaccion.ValorRetiro * 4 / 1000;
				}	
				_context.Add(transaccion);

				

				if (c.Gmf)
				{
					transaccion.ValorGmf = transaccion.ValorRetiro * 4 / 1000;
				}

				_context.Add(transaccion);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "NombreCompleto", transaccion.IdCliente);
			ViewData["IdCuentaDestino"] = new SelectList(_context.Cuentas, "Id", "NumCuenta", transaccion.IdCuentaDestino);
			ViewData["IdCuentaOrigen"] = new SelectList(_context.Cuentas, "Id", "NumCuenta", transaccion.IdCuentaOrigen);
			ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccion, "Id", "NombreTipoTran", transaccion.IdTipoTransaccion);
			return View(transaccion);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult IdCuentaOrigenIndexChanged()
		{
			List<Cuentas> Cuentas = _context.Cuentas.Where(c => c.Id != 1).ToList();
			ViewData["IdCuentaDestino"] = new SelectList(Cuentas);
			
			return View();
		}

	}
}
