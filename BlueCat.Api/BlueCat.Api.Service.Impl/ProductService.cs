using BlueCat.Api.Interface;
using BlueCat.Api.Service.Interface;
using BlueCat.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.ServiceImpl
{
    [Export(typeof(IProductService))]
    public abstract class ProductService : UnitOfWorkService,IProductService
    {
        [Import]
        protected IProductRepository ProductRepository { get; set; }

        public virtual CreateProductResponse CreateProduct(CreateProductRequest request)
        {
            return new CreateProductResponse();
        }

    }
}
