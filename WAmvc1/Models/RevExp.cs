using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAmvc1.Models
{
    public class RevExp
    {
        [Key]
        public int RevExpId { get; set; }

        public int? AccountId { get; set; }

        public DrAccount? Account { get; set; }

        public int? Expense { get; set; }
        public int? Revenue { get; set; }
        public int? Earning { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string? UserName { get; set; }

        [NotMapped]
        public string? AccountName
        {
            get
            {
                return Account != null ? Account.Title : "NotFound";
            }
        }
    }
}
