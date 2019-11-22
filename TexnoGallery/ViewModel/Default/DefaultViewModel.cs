using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TexnoGallery.Models;

namespace TexnoGallery.ViewModel.Default
{

    public class DefaultViewModel
    {
        public List<Slide> SlideImage;

        public AboutU aboutTech { get; set; }
        public Contact contactTech { get; set; }
        public List<Brend> BrendPhoto { get; set; }
        public IEnumerable<Product> productList { get; set; }
        public List<ProductImage> ProImage { get; set; }
        public Product ProductDetail { get; set; }
        public List<Category> CategoryName { get; set; }
        public List<SubCategory> SubCategoryName { get; set; }
        public List<Order> OrderList { get; set; }
        //public List<Produc> OptionPro;
        public List<Marka> marka{ get; set; }

        public ApplicationUser applicationUser { get; set; }
    }
}