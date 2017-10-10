using Sales.Model;
using System.ComponentModel.DataAnnotations;

namespace Sales.Web.ViewModels
{
    public class SalesOrderItemViewModel : IObjectWithState
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Server: Max 15 chars.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Server: Bad product code.")]
        public string ProductCode { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        [Required]
        [Range(1, 1000)]
        public decimal UnitPrice { get; set; }

        public int SalesOrderId { get; set; }

        public ObjectState ObjectState { get; set; }
    }
}
