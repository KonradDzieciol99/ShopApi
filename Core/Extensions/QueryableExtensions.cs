//using CloudinaryDotNet;
//using Stripe;
using Core.Entities;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
//using WorkflowApi.Entities;

namespace Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByPropertyOrField<T>(this IQueryable<T> queryable, string propertyOrFieldName, bool ascending = true)
        {
            var elementType = typeof(T);
            var orderByMethodName = ascending ? "OrderBy" : "OrderByDescending";

            var parameterExpression = Expression.Parameter(elementType);

            Expression propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, propertyOrFieldName);

            if (propertyOrFieldName == "price")
            {
                propertyOrFieldExpression = Expression.Condition(
                    Expression.NotEqual(Expression.Property(parameterExpression, "cutPrice"), Expression.Constant(null))
                   , Expression.Property(parameterExpression, "cutPrice")
                   , Expression.Convert(Expression.Property(parameterExpression, "Price")
                   , typeof(Nullable<decimal>))
                   );
            }
             
            var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);
            var orderByExpression = Expression.Call(typeof(Queryable), orderByMethodName,
                new[] { elementType, propertyOrFieldExpression.Type }, queryable.Expression, selector); ///<-- tutaj wywołuje  OrderBy albo OrderByDescending

            return queryable.Provider.CreateQuery<T>(orderByExpression);
        }
    }
}