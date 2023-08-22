using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using WAmvc1.Data;
using WAmvc1.Models;

namespace WAmvc1.Controllers
{
    public class LedgerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LedgerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ledger
        public async Task<IActionResult> Index(int id, DateTime date_b, DateTime date_e)
        {
            if (id == 0)
            {
                ViewBag.Date_B = DateTime.Now;
                ViewBag.Date_E = DateTime.Now;
            }
            else
            {
                ViewBag.Date_B = date_b;
                ViewBag.Date_E = date_e;
            }
            //            return View();
            var filteredJournalAllAccountsByUser = _context.Journals.
                            Where(p => p.UserName == User.Identity.Name).
                            Include(j => j.CrAccount).Include(j => j.DrAccount).ToList();
            var DrAccountsList = _context.DrAccounts.ToList();
            var filteredJournalAllAccountsByUserDuring = filteredJournalAllAccountsByUser.
                Where(p =>p.Date>=date_b && p.Date<=date_e ).ToList();
            List<Ledger> ledgers = new List<Ledger>();
            Ledger ledger10 = new Ledger();
            ledger10.Field1 = "LEDGER";
            ledger10.Field2 = "LEDGER";
            ledger10.Field3 = "LEDGER";
            ledgers.Add(ledger10);
            Ledger ledger4 = new Ledger();
            ledger4.Field1 = ledger4.Field2 = ledger4.Field3 = ledger4.Field4 = "---------------"; 

            List<Ledger> balances = new List<Ledger>();
            Ledger balance0 = new Ledger();
            balance0.Field1 = "BALANCE";
            balance0.Field2 = "BALANCE";
            balance0.Field3 = "BALANCE";
            balances.Add(balance0);
            Ledger balance2 = new Ledger();
            balance2.Field1 = "Account";
            balance2.Field2 = "Debit";
            balance2.Field3 = "Credit";
            balances.Add(balance2);

            List<Ledger> incomestatements = new List<Ledger>();
            Ledger incomestatement5 = new Ledger();
            incomestatement5.Field1 = "Income Statement";
            incomestatement5.Field2 = "Income Statement";
            incomestatement5.Field3 = "Income Statement";
            incomestatement5.Field4 = "Income Statement";
            incomestatements.Add(incomestatement5);
            Ledger incomestatement6 = new Ledger();
            incomestatement6.Field1 = "Account";
            incomestatement6.Field2 = "Revenue";
            incomestatement6.Field3 = "Expense";
            incomestatement6.Field4 = "Earning";
            incomestatements.Add(incomestatement6);

            int SumDrAmmountBalance=0, SumCrAmmountBalance=0,
                SumDrAmmountBalanceTotal=0, SumCrAmmountBalanceTotal = 0,
                SumRevenue=0, SumExpense=0, SumEarning=0;

            foreach (DrAccount acc in DrAccountsList)
            {
                if (acc.Title != "NotAccount")
                {
                    var filteredJournalSingleAccount = filteredJournalAllAccountsByUserDuring.
                        Where(p => p.CrAccountName == acc.Title || p.DrAccountName == acc.Title).
                        OrderBy(n => n.JournalId).ToList();

                    var SumDrAmmountBefore = filteredJournalAllAccountsByUser.
                        Where(p => p.Date < date_b && p.DrAccountName == acc.Title).Sum(p => p.DrAmmount);
                    var SumCrAmmountBefore = filteredJournalAllAccountsByUser.
                       Where(p => p.Date < date_b && p.CrAccountName == acc.Title).Sum(p => p.CrAmmount);

                    //            List<Journal> journal = new List<Journal>(filteredJournalSingleAccount);


                    Ledger ledger0 = new Ledger();
                    ledger0.Field2 = "Debit";
                    ledger0.Field3 = "Credit";
                    ledger0.Field1 = acc.Title;
                    ledgers.Add(ledger0);

                    Ledger ledger = new Ledger();
                    ledger.Field1 = "Beg balance";
                    ledger.Field2 = String.Format("{0:C}", SumDrAmmountBefore);
                    ledger.Field3 = String.Format("{0:C}", SumCrAmmountBefore); ;
                    ledgers.Add(ledger);

                    int SumDrAmmount = 0, SumCrAmmount = 0;
                    foreach (Journal j in filteredJournalSingleAccount)
                    {
                        Ledger ledger1 = new Ledger();
                        if (j.DrAccountName == acc.Title)
                        {
                            ledger1.Field1 = j.Date.ToShortDateString();
                            ledger1.Field2 = String.Format("{0:C}", j.DrAmmount);
                            ledger1.Field3 = String.Format("{0:C}", 0);
                            ledgers.Add(ledger1);
                            SumDrAmmount += (int)j.DrAmmount;
                        }
                        Ledger ledger2 = new Ledger();
                        if (j.CrAccountName == acc.Title)
                        {
                            ledger2.Field1 = j.Date.ToShortDateString();
                            ledger2.Field2 = String.Format("{0:C}", 0);
                            ledger2.Field3 = String.Format("{0:C}", j.CrAmmount);
                            ledgers.Add(ledger2);
                            SumCrAmmount += (int)j.CrAmmount;
                        }

                    }
                    SumDrAmmountBalance = SumDrAmmount > SumCrAmmount ? 
                        (int)SumDrAmmountBefore + SumDrAmmount - SumCrAmmount:0;
                    SumCrAmmountBalance = SumCrAmmount > SumDrAmmount ?
                        (int)SumCrAmmountBefore + SumCrAmmount - SumDrAmmount : 0;
                    Ledger ledger3 = new Ledger();
                    ledger3.Field1 = "Balance";
                    ledger3.Field2 = String.Format("{0:C}", SumDrAmmountBalance);
                    ledger3.Field3 = String.Format("{0:C}", SumCrAmmountBalance);
                    ledgers.Add(ledger3);
                    ledgers.Add(ledger4);

                    if (SumDrAmmountBalance != 0 || SumCrAmmountBalance != 0)
                    {
                        Ledger balance1 = new Ledger();
                        balance1.Field1 = acc.Title;
                        balance1.Field2 = String.Format("{0:C}", SumDrAmmountBalance);
                        balance1.Field3 = String.Format("{0:C}", SumCrAmmountBalance);
                        balances.Add(balance1);
                        SumDrAmmountBalanceTotal += SumDrAmmountBalance;
                        SumCrAmmountBalanceTotal += SumCrAmmountBalance;
                      
                        if (acc.RevExpAccount=="Revenue" || acc.RevExpAccount == "Expense")
                        {
                            int ValRevenue, ValExpense, ValEarning;
                            ValRevenue = acc.RevExpAccount == "Revenue" ? SumCrAmmountBalance : 0;
                            ValExpense = acc.RevExpAccount == "Expense" ? SumDrAmmountBalance : 0;
                            ValEarning = ValRevenue - ValExpense;
                            SumRevenue += ValRevenue;
                            SumExpense += ValExpense;
                            SumEarning += ValEarning;

                            Ledger incomestatement1 = new Ledger();
                            incomestatement1.Field1 = acc.Title;
                            incomestatement1.Field2 = String.Format("{0:C}", ValRevenue);
                            incomestatement1.Field3 = String.Format("{0:C}", ValExpense) ;
                            incomestatement1.Field4 = String.Format("{0:C}", ValEarning);
                            incomestatements.Add(incomestatement1);
                        }
                    }


                }

            }
            balances.Add(ledger4);
            Ledger balance3 = new Ledger();
            balance3.Field1 = "Total";
            balance3.Field2 = String.Format("{0:C}", SumDrAmmountBalanceTotal);
            balance3.Field3 = String.Format("{0:C}", SumCrAmmountBalanceTotal);
            balances.Add(balance3);
            balances.Add(ledger4);

            incomestatements.Add(ledger4);
            Ledger incomestatement3 = new Ledger();
            incomestatement3.Field1 = "Total";
            incomestatement3.Field2 = String.Format("{0:C}", SumRevenue);
            incomestatement3.Field3 = String.Format("{0:C}", SumExpense);
            incomestatement3.Field4 = String.Format("{0:C}", SumEarning);
            incomestatements.Add(incomestatement3);

            incomestatements.Add(ledger4);

            ledgers.AddRange(balances);
            ledgers.AddRange(incomestatements);
            return View(ledgers);

            //              return View(await _context.Ledgers.ToListAsync());
        }


        // POST: Ledger/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DateTime date_begin, DateTime date_end)
        {
            {
                 return RedirectToAction("Index", "Ledger", new { @id = 1, @date_b=date_begin, @date_e=date_end }) ;
            }

        }

    }
}
