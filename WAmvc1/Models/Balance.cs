using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAmvc1.Models
{
    public class Balance
    {
        [Key]
        public int BalanceId { get; set; }

        public int? AccountId { get; set; }

        public DrAccount? Account { get; set; }

        public int? DrAmmount { get; set; }

        public int? CrAmmount { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string? UserName { get; set; }

        [NotMapped]
        public string? AccountName
        {
            get { 
                return Account!=null? Account.Title : "NotFound"; }
        }



    }
}
