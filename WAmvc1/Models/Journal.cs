using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAmvc1.Models
{
    public class Journal
    {
        [Key]
        public int JournalId { get;set; }

        public int DrAccountId { get;set; }
        public DrAccount? DrAccount { get; set; }
        public int CrAccountId { get; set; }
        public CrAccount? CrAccount { get; set; }

        public int? DrAmmount { get; set; }

        public int? CrAmmount { get; set; }

        [Column(TypeName ="nvarchar(75)")]
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(450)")]
        public string? UserName { get;set; }
        [NotMapped]
        public string? DrAccountName
        {
            get
            {
                return DrAccount != null ? DrAccount.Title : "NotFound";
            }
        }
        public string? CrAccountName
        {
            get
            {
                return CrAccount != null ? CrAccount.Title : "NotFound";
            }
        }

    }
}
