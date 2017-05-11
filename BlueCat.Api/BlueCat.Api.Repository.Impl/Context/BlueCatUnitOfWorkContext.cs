using BlueCat.Api.UnitOfWork;
using System.ComponentModel.Composition;
using System.Data.Entity;

namespace BlueCat.Api.Repository.Impl.Context
{
    //[Export(typeof(IUnitOfWork))]
    public class BlueCatUnitOfWorkContext : UnitOfWorkContextBase
    {

        /// <summary>
        ///     获取或设置 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get { return BlueCatDbContext; }
        }

        /// <summary>
        ///     获取或设置 默认的Demo项目数据访问上下文对象
        /// </summary>
        //[Import(typeof(DbContext))]
        public BlueCatDbContext BlueCatDbContext{ get; set; }
    }
}
