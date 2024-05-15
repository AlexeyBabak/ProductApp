using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Infrastructure
{
    public class DBContextSettings
    {
        public string DefaultConnection { get; set; }

        public string ProductDbConnectionString { get; set; }
    }
}
