using BlueCat.Api.UnitOfWork;
using System.ComponentModel.Composition;

namespace BlueCat.Api.Service.Impl
{
   public class UnitOfWorkService
    {
        //[Import]
        protected IUnitOfWork UnitOfWork { get; set; }
    }
}
