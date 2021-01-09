using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSAProjectWeb.Server.Entities
{
    public class DataRequest<T> 
    {
        public T Data { get; set; }
    }
}
