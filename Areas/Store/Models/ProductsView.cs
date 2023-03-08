using DStore.Models;

namespace DStore.Areas.Store.Models
{
    public class ProductsView
    {
        public Product Data { get; set; }

        public List<ImageData> Image { get; set; }
    }
}
