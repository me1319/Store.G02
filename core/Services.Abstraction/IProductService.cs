using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        // service is a class we write the 4 main methods imp in
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();
        Task<ProductResultDto?> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
