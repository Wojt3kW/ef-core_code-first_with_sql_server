using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemoApp.DBModels
{
    [ComplexType]
    public class Address
    {
        [MaxLength(500)]
        public required string Line1 { get; set; }
        [MaxLength(500)]
        public string? Line2 { get; set; }
        [MaxLength(100)]
        public required string City { get; set; }
        [MaxLength(100)]
        public required string Country { get; set; }
        [MaxLength(100)]
        public required string PostCode { get; set; }
    }
}
