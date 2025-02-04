using Fiorella.Helpers;
using Fiorella.Helpers.Extentions;
using Fiorella.Helpers.Requests;
using Fiorella.Models;
using Fiorella.Services.Interfaces;
using Fiorella.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public ProductController(IProductService productService,ICategoryService categoryService,IWebHostEnvironment env)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
        }
        [HttpGet]
        public async Task< IActionResult > Index(int page=1)
        {
            var dbProducts=await _productService.GetAllAsync();
            var paginateDatas = await _productService.GetAllPaginateAsync(page);
            List<ProductVM> mappedDatas=_productService.GetMappedDatas(paginateDatas);
           int pageCount =await GetPageCountAsync(4);
            Paginate<ProductVM> model = new(mappedDatas, pageCount, page);
            return View(model);
        }
        private async Task<int>GetPageCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)count / take);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Product product=await _productService.GetByIdAsync((int)id);
            if (product is null) return NotFound(); 
            List<ProductImageVM> productImages = new();
            foreach (var item in product.ProductImages)
            {
                productImages.Add(new ProductImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain,
                });
            }
            ProductDetailVM model = new()
            {
                Name = product.Name,
                Price = product.Price,
                Category = product.Category.Name,
                Images = productImages
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories=await _categoryService.GetAllBySelectedAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(ProductCreateVM request)
        {
            ViewBag.categories = await _categoryService.GetAllBySelectedAsync();
            if (!ModelState.IsValid)
            {
                return View();
            } 
            foreach(var item in request.Images)
            {
                if (!item.CheckFileSize(2000))
                {
                    ModelState.AddModelError("Images", "Image size must be max 500kb");
                    return View();
                }
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be only image");
                    return View();
                }
            }
            List<ProductImages> images = new();
            foreach (var item in request.Images)
            {
                string fileName=Guid.NewGuid().ToString()+"-"+item.FileName;
                string path=Path.Combine(_env.WebRootPath,"assets","img",fileName);
                await item.SaveFileToLocalAsync(path);
                images.Add(new ProductImages
                {
                    Name = fileName,
                });
            }
            images.FirstOrDefault().IsMain=true;
            Product product = new()
            {
                Name = request.Name,
                Price = decimal.Parse(request.Price.Replace(".",",")),
                CategoryId = request.CategoryId,
                ProductImages = images
            };

            await _productService.CreateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            Product product =await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();
            foreach (var item in product.ProductImages)
            {
                string path = Path.Combine(_env.WebRootPath, "assets", "img", item.Name);
                path.DeleteFileFromLocal();
            }
            await _productService.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(DeleteProductImageRequest request)
        {
            await _productService.DeleteProductImageAsync(request);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.categories = await _categoryService.GetAllBySelectedAsync();
            if (id == null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();
            ProductEditVM response = new()
            {
                Name = product.Name,
                Price = product.Price.ToString().Replace(",","."),
                CategoryId = product.CategoryId,
                ExistImage = product.ProductImages.Select(m => new ProductEditImageVM
                {
                    Id = m.Id,
                    ProductId = m.ProductId,
                    Image = m.Name,
                    IsMain = m.IsMain
                }).ToList()
            };
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,ProductEditVM request)
        {
            ViewBag.categories = await _categoryService.GetAllBySelectedAsync();
            if (id == null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();

            if (ModelState.IsValid)
            {
                request.ExistImage = product.ProductImages.Select(m => new ProductEditImageVM
                {
                    Id = m.Id,
                    ProductId = m.ProductId,
                    Image = m.Name,
                    IsMain = m.IsMain
                }).ToList();
                return View(request);
            }
            
            if (request.NewImages is not null )
            {
                foreach (var item in request.NewImages)
                {
                    if (!item.CheckFileSize(2000))
                    {
                        request.ExistImage = product.ProductImages.Select(m => new ProductEditImageVM
                        {
                            Id = m.Id,
                            ProductId = m.ProductId,
                            Image = m.Name,
                            IsMain = m.IsMain
                        }).ToList();
                        ModelState.AddModelError("NewImages", "Image size must be max 500kb");
                        return View(request);
                    }
                    if (!item.CheckFileType("image/"))
                    {
                        request.ExistImage = product.ProductImages.Select(m => new ProductEditImageVM
                        {
                            Id = m.Id,
                            ProductId = m.ProductId,
                            Image = m.Name,
                            IsMain = m.IsMain
                        }).ToList();
                        ModelState.AddModelError("NewImages", "File type must be only image");
                        return View(request);
                    }
                }
            }
            await _productService.EditAsync(product, request);
            return RedirectToAction(nameof(Index));
        }
    }
}
