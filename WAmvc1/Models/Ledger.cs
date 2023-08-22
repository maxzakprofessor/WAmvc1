using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAmvc1.Models
{
    public class Ledger
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Field1 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Field2 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Field3 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Field4 { get; set; }
    }
}
