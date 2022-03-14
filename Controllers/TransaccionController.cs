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
		public async Task<IActionResult> Index()
		{
			var appTransferenciaDbContext = _context.Transaccion.Include(t => t.IdClienteNavigation).Include(t => t.IdCuentaDestinoNavigation).Include(t => t.IdCuentaOrigenNavigation).Include(t => t.IdTipoTransaccionNavigation);
			return View(await appTransferenciaDbContext.ToListAsync());	
		}
		// GET: Transaccion
		public IActionResult CrearTransaccion()
		{
			ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "NombreCompleto");
			ViewData["IdCuentaDestino"] = new SelectList(_context.Cuentas, "Id", "NumCuenta");
			ViewData["IdCuentaOrigen"] = new SelectList(_context.Cuentas, "Id", "NumCuenta");
			ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccion, "Id", "NombreTipoTran");
			return View();
		}

		// GET: Transaccion
		public IActionResult Retirar()
		{
			ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "NombreCompleto");			
			ViewData["IdCuentaOrigen"] = new SelectList(_context.Cuentas, "Id", "NumCuenta");
			ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccion, "Id", "NombreTipoTran");
			return View();
		}
		// POST: Metodo Crear Nueva Transaccón
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CrearTransaccion([Bind("Id,IdTipoTransaccion,ValorGmf,IdCliente,ValorRetiro,IdCuentaOrigen,IdCuentaDestino")] Transaccion transaccion)
		{

			if (ModelState.IsValid)
			{
				Cuentas cuenta = new Cuentas();
				TipoTransaccion tipoTransa = _context.TipoTransaccion.Where(c => c.Id == 1).FirstOrDefault();

				if (tipoTransa.NombreTipoTran == "Transferencia")
				{
					cuenta = _context.Cuentas.Where(c => c.Id == transaccion.IdCuentaDestino).FirstOrDefault();
					cuenta.Saldo = cuenta.Saldo + transaccion.ValorRetiro.GetValueOrDefault();
					_context.Update(cuenta);
				}
				cuenta = _context.Cuentas.Where(c => c.Id == transaccion.IdCuentaOrigen).FirstOrDefault();
				cuenta.Saldo = cuenta.Saldo - transaccion.ValorRetiro.GetValueOrDefault();
				_context.Update(cuenta);
				Clientes c = _context.Clientes.Where(c => c.Id == transaccion.IdCliente).FirstOrDefault();
				if (c.Gmf)
				{
					transaccion.ValorGmf = (transaccion.ValorRetiro * 4) / 1000;
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Retirar([Bind("Id,IdTipoTransaccion,ValorGmf,IdCliente,ValorRetiro,IdCuentaOrigen,FechaTransacción")] Transaccion transaccion)
		{

			if (ModelState.IsValid)
			{
				Cuentas cuenta = new Cuentas();
				TipoTransaccion tipoTransa = _context.TipoTransaccion.Where(c => c.Id == 2).FirstOrDefault();
				
				cuenta = _context.Cuentas.Where(c => c.Id == transaccion.IdCuentaOrigen).FirstOrDefault();
				cuenta.Saldo = cuenta.Saldo - transaccion.ValorRetiro.GetValueOrDefault();
				_context.Update(cuenta);
				Clientes c = _context.Clientes.Where(c => c.Id == transaccion.IdCliente).FirstOrDefault();
				if (c.Gmf)
				{
					transaccion.ValorGmf = (transaccion.ValorRetiro * 4) / 1000;
				}
				_context.Add(transaccion);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "NombreCompleto", transaccion.IdCliente);			
			ViewData["IdCuentaOrigen"] = new SelectList(_context.Cuentas, "Id", "NumCuenta", transaccion.IdCuentaOrigen);
			ViewData["IdTipoTransaccion"] = new SelectList(_context.TipoTransaccion, "Id", "NombreTipoTran", transaccion.IdTipoTransaccion);
			return View(transaccion);
		}
	}
}
