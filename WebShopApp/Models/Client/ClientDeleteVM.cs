using System.ComponentModel.DataAnnotations;

namespace WebShopApp.Models.Client
{
    public class ClientDeleteVM
    {

        public string Id { get; set; } = null!;

        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;
        [Display(Name = "FirstName")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "LastName")]
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        

    }
}
