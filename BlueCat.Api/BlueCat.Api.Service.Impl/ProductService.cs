using BlueCat.Api.Repository.Interface;
using BlueCat.Api.Service.Interface;
using BlueCat.Api.UnitOfWork;
using BlueCat.Contract;
using System.ComponentModel.Composition;
using System.Linq;

namespace BlueCat.Api.Service.Impl
{
    //public class ProductService : UnitOfWorkService, IProductService
    public class ProductService : IProductService
    {

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.ProductRepository = productRepository;
            this.UnitOfWork = unitOfWork;
        }

        protected IProductRepository ProductRepository { get; set; }

        protected IUnitOfWork UnitOfWork { get; set; }

        public  CreateProductResponse CreateProduct(CreateProductRequest request)
        {
            Product product = ProductRepository.Entities.SingleOrDefault(x => x.Id == request.Id);

             Product product1=new Product();
            product1.Id=5;
             product1.Name="111";
             product1.Category="222";
             product1.Id=5;

             ProductRepository.Insert(product1);

            return new CreateProductResponse();
        }

    }
}
