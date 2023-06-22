using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Descripition")]
        public string Description { get; set; }
        [Display(Name = "ImageURL")]
        public string ImageURl { get; set; }
        [Display(Name = "Price")]
        public float Price { get; set; }
    }
}
