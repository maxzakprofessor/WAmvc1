using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WAmvc1.Data;
using WAmvc1.Models;

namespace WAmvc1.Controllers
{
    public class DrAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DrAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DrAccount
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "teacher@finac.kz")
                return View(await _context.DrAccounts.OrderByDescending(n => n.DrAccountId).ToListAsync());
            return View();
        }

        // GET: DrAccount/Details/5
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "teacher@finac.kz")
            {
                if (id == 0)
                return View(new DrAccount());
            else return View(await _context.DrAccounts.FindAsync(id));
            }
            return RedirectToAction(nameof(Index));

        }


        // POST: DrAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("DrAccountId,Title,RevExpAccount")] DrAccount drAccount)
        {
            CrAccount crAccount = new CrAccount()
            {
                CrAccountId = drAccount.DrAccountId,
                Title = drAccount.Title,
                RevExpAccount = drAccount.RevExpAccount
            };
            if (ModelState.IsValid)
            {

                if (drAccount.DrAccountId == 0) {
                    _context.DrAccounts.Add(drAccount);
                }
                else {
                    _context.DrAccounts.Update(drAccount);
                }
                    await _context.SaveChangesAsync();
                if (crAccount.CrAccountId == 0){
                    _context.CrAccounts.Add(crAccount);
                }
                else {
                    _context.CrAccounts.Update(crAccount);
                }
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(drAccount);
        }

 

        // POST: DrAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DrAccounts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DrAccounts'  is null.");
            }
            var draccounts = await _context.DrAccounts.FindAsync(id);
            var craccounts = await _context.CrAccounts.FindAsync(id);
            if (draccounts != null && craccounts != null)
            {
                _context.DrAccounts.Remove(draccounts);
                _context.CrAccounts.Remove(craccounts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
