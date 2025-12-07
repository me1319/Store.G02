using AutoMapper;
using Domain.Contracts;
//using Persistence;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketRepository repository,
        ICacheRepository cacheRepository
        ):IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService basketService { get; } = new BasketService(repository, mapper);
        public ICacheServices cacheServices { get; } = new CacheServices(cacheRepository);

    }
}
