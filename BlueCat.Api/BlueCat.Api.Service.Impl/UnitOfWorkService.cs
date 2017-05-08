using BlueCat.Api.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.ServiceImpl
{
   public class UnitOfWorkService
    {
        [Import]
        protected IUnitOfWork UnitOfWork { get; set; }
    }
}
