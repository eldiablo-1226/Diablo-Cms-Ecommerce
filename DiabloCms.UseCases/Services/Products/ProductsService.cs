using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Products;
using DiabloCms.Models.ResponseModel.Products;
using DiabloCms.MsSql;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.File;
using DiabloCms.UseCases.Contracts.Products;
using DiabloCms.UseCases.Services.Products.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Services.Products
{
    using static ErrorMessagesService;

    public class ProductsService : BaseService<Product>, IProductsService
    {
        private const int ProductsPerPage = 12;

        private readonly IFilesManagerService _filesManager;

        public ProductsService(CmsDbContext dbContext, IMapper mapper, IFilesManagerService filesManager)
            : base(dbContext, mapper)
        {
            _filesManager = filesManager;
        }

        #region Get Product

        public async Task<ProductsDetailsResponseModel> DetailsAsync(Guid id)
        {
            var products = AllAsNoTracking
                .Where(p => p.Id == id);

            var result = await Mapper
                .ProjectTo<ProductsDetailsResponseModel>(products)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return result;
        }

        public async Task<ProductsSearchResponseModel> ProductSearchAsync(ProductsSearchRequestModel model)
        {
            var specification = GetProductSpecification(model);

            var products = await Mapper
                .ProjectTo<ProductsListingResponseModel>(AllAsNoTracking
                    .Where(specification)
                    .Skip((model.Page - 1) * ProductsPerPage)
                    .Take(ProductsPerPage))
                .ToListAsync()
                .ConfigureAwait(false);

            var totalPages = await GetTotalPages(model).ConfigureAwait(false);

            return new ProductsSearchResponseModel
            {
                Products = products,
                Page = model.Page,
                TotalPages = totalPages
            };
        }

        #endregion Get Product

        #region Product

        public async Task<Result> CreateProductAsync(ProductsRequestModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Article = model.Article,
                IsActive = model.IsActive,
                IsVariable = model.IsSingleAttribute,
                VideoUrl = model.VideoUrl,
                CategoryId = model.CategoryId
            };

            await Data.Products.AddAsync(product)
                .ConfigureAwait(false);

            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> UpdateProductAsync(UpdateProductRequestModel model, Guid id)
        {
            var product = await All.FirstOrDefaultAsync(p => p.Id == id).ConfigureAwait(false);

            if (product == null) return NotFound;

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Article = model.Article;
            product.IsActive = model.IsActive;
            product.IsVariable = model.IsSingleAttribute;
            product.VideoUrl = model.VideoUrl;
            product.CategoryId = Guid.Parse(model.CategoryId);

            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> RemoveProductAsync(Guid id)
        {
            var product = await All.FirstOrDefaultAsync(p => p.Id == id).ConfigureAwait(false);

            if (product == null) return NotFound;

            Data.Remove(product);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        #endregion Product

        #region Product Attribute

        public async Task<Result> AddProductAttributeAsync(AttributeResponseModel model, Guid productId,
            bool saveChange = true)
        {
            var productAttribute = new ProductAttribute
            {
                ProductId = productId,
                Name = model.Name,
                Color = model.Color,
                Size = model.Size,
                Photo = model.Photo,
                StockQuantity = model.StockQuantity,
                Price = model.Price
            };

            await Data.ProductAttribute.AddAsync(productAttribute).ConfigureAwait(false);

            if (saveChange)
                await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> UpdateProductAttributeAsync(AttributeResponseModel model, Guid id)
        {
            var productAttribute = await Data.ProductAttribute.FindAsync(id).ConfigureAwait(false);

            if (productAttribute == null) return NotFound;

            productAttribute.Name = model.Name;
            productAttribute.Color = model.Color;
            productAttribute.Size = model.Size;
            productAttribute.Photo = model.Photo;
            productAttribute.StockQuantity = model.StockQuantity;
            productAttribute.Price = model.Price;

            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> RemoveProductAttributeAsync(Guid id)
        {
            var attribute = await Data.ProductAttribute.FindAsync(id).ConfigureAwait(false);

            if (attribute == null) return NotFound;

            Data.ProductAttribute.Remove(attribute);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        #endregion Product Attribute

        #region Product Photo

        public async Task<Result> AddProductPhotoAsync(Stream stream, Guid productId, string fileName)
        {
            var result = await _filesManager
                .UploadPhoto(stream, fileName)
                .ConfigureAwait(false);

            if (!result.Succeeded) return Result.Failure(result.Errors);

            var photo = new PhotoUrl
            {
                ProductId = productId,
                Files = new Entities.Models.Files {Url = result.Data}
            };

            await Data.PhotoUrl.AddAsync(photo)
                .ConfigureAwait(false);
            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> RemoveProductPhotoAsync(Guid id)
        {
            var photo = await Data.PhotoUrl
                .Where(x => x.Id == id)
                .Include(x => x.Files)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (photo == null) return NotFound;

            await _filesManager
                .DeleteFileAsync(photo.Files.Url)
                .ConfigureAwait(false);

            Data.Files.Remove(photo.Files);
            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        #endregion

        #region Other

        private static Specification<Product> GetProductSpecification(ProductsSearchRequestModel model)
        {
            return new ProductByNameSpecification(model.Query)
                //.And(new ProductByPriceSpecification(model.MinPrice, model.Price))
                .And(new ProductByCategorySpecification(model.Category))
                .And(new ProductByColorSpecification(model.Color))
                .And(new ProductBySizeSpecification(model.Size));
        }

        private async Task<int> GetTotalPages(ProductsSearchRequestModel model)
        {
            var specification = GetProductSpecification(model);

            var total = await AllAsNoTracking
                .Where(specification)
                .CountAsync()
                .ConfigureAwait(false);

            return (int) Math.Ceiling((double) total / ProductsPerPage);
        }

        #endregion
    }
}