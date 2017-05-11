using BlueCat.Api.Repository.Impl.Context;
using BlueCat.Api.Repository.Interface;
using BlueCat.Api.UnitOfWork;
using BlueCat.Contract;
using System.ComponentModel.Composition;

namespace BlueCat.Api.Repository.Impl
{
    public class ProductRepository : EFRepositoryBase<Product>, IProductRepository 
    {
        protected IUnitOfWork BlueCatUnitOfWorkContext { get; set; }
        public ProductRepository(IUnitOfWork unitOfWork)
        {
            this.BlueCatUnitOfWorkContext = unitOfWork;
        }
       
    }
}
