using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanCode.Core.Models
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public PagedList()
        {
        }

        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize, string sortby, string sortOrder, string filterby, string filterValue)
        {
            if (!string.IsNullOrEmpty(sortby))
            {
                IOrderedQueryable<T> orderedList = ApplyOrderDirection(items.AsQueryable(), sortby, sortOrder);
                items = orderedList.AsEnumerable();
            }
            if (!string.IsNullOrEmpty(filterby) && !string.IsNullOrEmpty(filterValue))
            {
                string pasclaFilter = char.ToUpper(filterby[0]) + filterby.Substring(1);
                PropertyInfo pInfo = items.First().GetType().GetProperty(pasclaFilter);
                Type type = pInfo.PropertyType;
                object filterValueAsObj= Convert.ChangeType(filterValue,type, CultureInfo.InvariantCulture); 
                items = items.Where(c => object.Equals(pInfo.GetValue(c), filterValueAsObj));
                //items = items.Where(t => t.GetType().GetProperty(pascalPropertyName).GetValue(t) == obj);
            }
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            HasPreviousPage = CurrentPage > 1;
            HasNextPage = CurrentPage < TotalPages;
            AddRange(items);
        }

        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize, string sortby, string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortby))
            {
                IOrderedQueryable<T> orderedList = ApplyOrderDirection(items.AsQueryable(), sortby, sortOrder);
                items = orderedList.AsEnumerable();
            }
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            HasPreviousPage = CurrentPage > 1;
            HasNextPage = CurrentPage < TotalPages;
            AddRange(items);
        }

        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            HasPreviousPage = CurrentPage > 1;
            HasNextPage = CurrentPage < TotalPages;
            AddRange(items);
        }

        private IOrderedQueryable<TSource> ApplyOrderDirection<TSource>(IQueryable<TSource> source, string attributeName, string sortOrder)
        {
            if (String.IsNullOrEmpty(attributeName))
            {
                return source as IOrderedQueryable<TSource>;
            }

            var propertyInfo = typeof(TSource).GetProperty(attributeName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new ArgumentException("ApplyOrderDirection: The associated Attribute to the given AttributeName could not be resolved", attributeName);
            }

            Expression<Func<TSource, object>> orderExpression = x => propertyInfo.GetValue(x, null);

            if (sortOrder.ToLower() == "asc")
            {
                return source.OrderBy(orderExpression);
            }
            else
            {
                return source.OrderByDescending(orderExpression);
            }
        }


        //public IOrderedQueryable<T> OrderBy<T>(IQueryable<T> query, string memberName)
        //{
        //    ParameterExpression[] typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };

        //    System.Reflection.PropertyInfo pi = typeof(T).GetProperty(memberName);

        //    return (IOrderedQueryable<T>)query.Provider.CreateQuery(
        //        Expression.Call(
        //            typeof(Queryable),
        //            "OrderBy",
        //            new Type[] { typeof(T), pi.PropertyType },
        //            query.Expression,
        //            Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
        //    );
        //}
    }
}
