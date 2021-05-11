using System;
using System.IO;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Products;
using DiabloCms.Models.ResponseModel.Products;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.Products
{
    public interface IProductsService
    {
        Task<Result> CreateProductAsync(ProductsRequestModel model);

        Task<Result> AddProductAttributeAsync(AttributeResponseModel model, Guid productId, bool saveChange = true);

        Task<Result> AddProductPhotoAsync(Stream stream, Guid productId, string fileName);

        Task<Result> UpdateProductAsync(UpdateProductRequestModel model, Guid id);

        Task<Result> UpdateProductAttributeAsync(AttributeResponseModel model, Guid id);

        Task<Result> RemoveProductAsync(Guid id);

        Task<Result> RemoveProductAttributeAsync(Guid id);

        Task<Result> RemoveProductPhotoAsync(Guid id);

        Task<ProductsDetailsResponseModel> DetailsAsync(Guid id);

        Task<ProductsSearchResponseModel> ProductSearchAsync(ProductsSearchRequestModel model);
    }
}