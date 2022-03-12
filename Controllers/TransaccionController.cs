using AppTransferencia.AppTransferencia.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace AppTransferencia.Controllers
{
	public class TransaccionController : Controller
	{
		private readonly AppTransferenciaDbContext _context;

		public TransaccionController(AppTransferenciaDbContext context)
		{
			context = _context;
		}

		public bool transferir(BigInteger numCuenta, double cantidad)
		{
			bool valid = false;
			//foreach (var cuenta in )
			//{
			//	if (cuenta.numCuenta == numCuenta)
			//	{
					
			//	}
			//}
			return valid;
		}

		//public IActionResult Index()
		//{
		//	return View();
		//}
	}
}
