using DStore.Areas.Store.Models;
using DStore.Models;
using DStore.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace DStore.Areas.Store.Controllers
{
    [Area("Store")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ProductsService _productsService;
        private readonly ImagesService _imagesService;
        private readonly ILogger _logger;
        public HomeController(
           ProductsService productsService,
           ImagesService imagesService,
           ILoggerFactory loggerFactory)

        {
            _productsService = productsService;
            _imagesService = imagesService;
            _logger = loggerFactory.CreateLogger<HomeController>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Product(int? page)
        {
                 
            var products = await _productsService.GetAllAsync();
            var list = new List<ProductsView>();

            foreach (var product in products)
            {

                List<ImageData> images = new List<ImageData>();
                foreach (var img in product.Images)
                {
                    var imgid = await _imagesService.GetByIdAsync(img.ToString());
                    images.Add(imgid);
                }
                var productsView = new ProductsView
                {
                    Data = product,
                    Image = images

                };
                list.Add(productsView);
            }

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = list.ToPagedList(pageNumber, 25);// will only contain 25 products max because of the pageSize
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}