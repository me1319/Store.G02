using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    // this class is responsiple for creating dynamic query
    static class SpecificationEvaluator
    {

        public static IQueryable<TEntity> GetQuery<TEntity ,TKey>
            (IQueryable<TEntity> inputQuery,ISpecifications<TEntity, TKey> spec)
            where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;

            // Criteria
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            // Ordering
            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            //Pagination
            if (spec.IsPagination)
                query = query.Skip(spec.Skip).Take(spec.Take); 

                query = spec.IncludeExpressions.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));


            return query;
        }
    }
}
