using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Core.Config
{
    public class DevelopmentSettings
    {
        public string DefaultAdminUserEmail { get; set; }

        public string DefaultAdminUserPassword { get; set; }

        public int WebpackDevServerPort { get; set; }

    }
}
