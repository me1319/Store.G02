using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        //ctor for one product 
        public ProductWithBrandsAndTypesSpecifications(int id) : base(P => P.Id == id)
        {
            ApplayInclude();


        }
        //ctor for all products
        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationParamters specParams ) :
            base(
                P => (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search.ToLower())) &&
                (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId)
                &&  (!specParams.TypeId.HasValue || P.TypeId == specParams.TypeId) 
                )
        {
            ApplayInclude();
            ApplySort(specParams.Sort);
            ApplyPagination(specParams.PageIndex, specParams.PageSize);

        }
        //method to use this code multiple times
        private void ApplayInclude()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
        private void ApplySort(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }


            }
            else { AddOrderBy(p => p.Name); }
        }
        protected void ApplyPagination(int pageIndex,int pageSize)
        {
            IsPagination = true;
            Take = pageSize;
            Skip= (pageIndex-1)*pageSize;

        }
    }
}
