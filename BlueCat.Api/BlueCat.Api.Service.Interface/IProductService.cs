using BlueCat.Contract;

namespace BlueCat.Api.Service.Interface
{
    public interface IProductService
    {
        CreateProductResponse CreateProduct(CreateProductRequest request);
    }
}
