using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTransferencia.AppTransferencia.Data;
using AppTransferencia.Models;

namespace AppTransferencia.Controllers
{
    public class BancosController : Controller
    {
        private readonly AppTransferenciaDbContext _context;

        public BancosController(AppTransferenciaDbContext context)
        {
            _context = context;
        }

        // GET: Bancos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bancos.ToListAsync());
        }

        // GET: Bancos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bancos = await _context.Bancos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bancos == null)
            {
                return NotFound();
            }

            return View(bancos);
        }

        // GET: Bancos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bancos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreBanco")] Bancos bancos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bancos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bancos);
        }

        // GET: Bancos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bancos = await _context.Bancos.FindAsync(id);
            if (bancos == null)
            {
                return NotFound();
            }
            return View(bancos);
        }

        // POST: Bancos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreBanco")] Bancos bancos)
        {
            if (id != bancos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bancos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BancosExists(bancos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bancos);
        }

        // GET: Bancos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bancos = await _context.Bancos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bancos == null)
            {
                return NotFound();
            }

            return View(bancos);
        }

        // POST: Bancos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bancos = await _context.Bancos.FindAsync(id);
            _context.Bancos.Remove(bancos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BancosExists(int id)
        {
            return _context.Bancos.Any(e => e.Id == id);
        }
    }
}
