using eCommerce.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    public class NewProductVM
    {

        
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

       
        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }


        
        [Display(Name = "Product poster URL")]
        [Required(ErrorMessage = "Product poster URL is required")]
        public string ImageURl { get; set; }


        
        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Price is required in USD")]
        public float Price { get; set; }
    }
}
