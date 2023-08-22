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
    public class JournalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JournalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Journals
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var applicationDbContext = _context.Journals.Where(p => p.UserName == User.Identity.Name).
                    Include(j => j.CrAccount).Include(j => j.DrAccount);

                return View(await applicationDbContext.OrderByDescending(n => n.Date).ToListAsync());
                
            }
            
            return View();
        }

        // GET: Journals/Details/5
        public async Task<IActionResult> AddOrEdit(int id=0)
        {
            if (User.Identity.IsAuthenticated)
            {
                PopulateAccounts();
                if (id == 0)
                {
                    Journal journal = new Journal() {
                        CrAccountId = 24, DrAccountId = 24,
                        CrAmmount = 0, DrAmmount = 0
                        };
                    return View(journal);
                }
                else
                    return View(await _context.Journals.FindAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }


        // POST: Journals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("JournalId,DrAccountId,CrAccountId,DrAmmount,CrAmmount,Note,Date,UserName")] Journal journal)
        {
            
            journal.UserName = User.Identity.Name;
            if (journal.DrAmmount == null) journal.DrAmmount = 0;
            if (journal.CrAmmount == null) journal.CrAmmount = 0;
            if (ModelState.IsValid)
            {
                if (journal.JournalId == 0)
                    _context.Add(journal);
                else
                    _context.Update(journal);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", transaction.CategoryId);
            PopulateAccounts();
            return View(journal);
        }


        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Journals == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Journals'  is null.");
            }
            var journal = await _context.Journals.FindAsync(id);
            if (journal != null)
            {
                _context.Journals.Remove(journal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public void PopulateAccounts()
        {


            var DrAccountCollection = _context.DrAccounts.ToList();
//            DrAccount DefaultDrAccount = new DrAccount() { DrAccountId = 0, Title = "Choose a DrAccount" };
//            DrAccountCollection.Insert(0, DefaultDrAccount);
            ViewBag.DrAccounts = DrAccountCollection;

            var CrAccountCollection = _context.CrAccounts.ToList();
//            CrAccount DefaultCrAccount = new CrAccount() { CrAccountId = 0, Title = "Choose a CrAccount" };
//            CrAccountCollection.Insert(0, DefaultCrAccount);
            ViewBag.CrAccounts = CrAccountCollection;
        }
    }
}
