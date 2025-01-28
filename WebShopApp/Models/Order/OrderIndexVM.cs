using System.ComponentModel.DataAnnotations;

namespace WebShopApp.Models.Order
{
    public class OrderIndexVM
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public string Product { get; set; } = null!;
        public int Quantity { get; set; }
        public string User { get; set; }
        public string OrderDate { get; set; }

        public string Picture { get; set; }
        [Range(1, 100)]
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }

    }
}

