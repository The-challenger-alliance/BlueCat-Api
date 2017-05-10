using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.Common
{
    public class GlobalVariable
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["BlueCatConnectionString"].ToString();
    }
}
