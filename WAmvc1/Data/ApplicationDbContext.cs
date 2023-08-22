using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WAmvc1.Models;

namespace WAmvc1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DrAccount> DrAccounts { get; set; }
        public DbSet<CrAccount> CrAccounts { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Balance> Balances  { get; set; }
        public DbSet<RevExp> RevExps  { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }

    }
}