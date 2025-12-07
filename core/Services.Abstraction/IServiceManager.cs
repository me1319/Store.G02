using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
         IProductService ProductService { get; }
        IBasketService basketService { get; }
        ICacheServices cacheServices { get; }
    }
}
