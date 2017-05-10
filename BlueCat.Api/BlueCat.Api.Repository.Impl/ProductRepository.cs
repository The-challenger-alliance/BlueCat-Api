using BlueCat.Api.Repository.Interface;
using BlueCat.Api.UnitOfWork;
using BlueCat.Contract;
using System.ComponentModel.Composition;

namespace BlueCat.Api.Repository.Impl
{
    public class ProductRepository : EFRepositoryBase<Product>, IProductRepository 
    {
        //public ProductRepository(IUnitOfWork unitOfWork)
        //{
        //    base.UnitOfWork = unitOfWork;
        //}
       
        //public StuEducationRepo(IDatabaseFactory databaseFactory)
        //    : base(databaseFactory)
        //{

        //}
    }
}
