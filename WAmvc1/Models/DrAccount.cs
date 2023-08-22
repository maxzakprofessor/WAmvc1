using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAmvc1.Models
{
    public class DrAccount
    {
        [Key]
        public int DrAccountId { get; set; }
        [Column (TypeName ="nvarchar(50)")]
        public string? Title { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? RevExpAccount { get; set; } = "No";

    }
}
