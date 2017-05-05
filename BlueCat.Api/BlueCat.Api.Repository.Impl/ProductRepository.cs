using BlueCat.Api.Interface;
using BlueCat.Api.UnitOfWork;
using BlueCat.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.Repository.Impl
{
    [Export(typeof(IProductRepository))]
    public class ProductRepository : EFRepositoryBase<Product>, IProductRepository { }
}
