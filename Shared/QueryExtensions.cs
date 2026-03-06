using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace Shared
{
    public static class QueryExtensions
    {
        //public static IQueryable<TSource> SelectSelfAndNested<TSource>(this IQueryable<TSource> source, Func<TSource, int?> parentId) where TSource : class
        //{
        //    return source
        //        .Where(x => parentId(x) == null)
        //        .SelectNested(parentId);

        //}

        public static IQueryable<TSource> SelectRecursive<TSource>(
            this DbSet<TSource> source,
            DbContext context,
            Expression<Func<TSource, bool>> condition,
            Expression<Func<TSource, object>> selector,
            Expression<Func<TSource, object>> parentSelector,
            Expression<Func<TSource, bool>>? finalCondition = null) where TSource : class
        {
            var entity = context.Model.FindEntityType(typeof(TSource));
            if (entity == null)
                throw new Exception($"Failed to find Database entity {nameof(TSource)}");
            var tableName = entity.GetTableName();
            var tableSchema = entity.GetSchema() ?? "dbo";
            var cols = entity.GetProperties().Select(x => x.GetColumnName()).ToList();
            var baseCondition = source.Where(condition).ToQueryString();
            var finalWhere = "";
            if (finalCondition != null)
                finalWhere += source.Where(finalCondition).ToQueryString();

            var targetColumnName = GetPropertyOfThrowException(entity, selector.GetPropertyName()).GetColumnName();
            var parentColumnName = GetPropertyOfThrowException(entity, parentSelector.GetPropertyName()).GetColumnName();

            return source
                .FromSqlRaw($@"WITH Tree AS (
                    SELECT {string.Join(", ", $"[{cols}]")} FROM [{tableSchema}].[{tableName}] {baseCondition}
                    UNION ALL
                    SELECT {string.Join(", ", cols.Select(c => "c.[" + c + "]"))} 
                    FROM {tableName} c
                    JOIN Tree t ON c.{parentColumnName} = t.{targetColumnName}
                )
                SELECT * FROM Tree {finalWhere};");

            static IProperty GetPropertyOfThrowException(IEntityType entity, string propertyName)
            {
                var prop = entity.FindProperty(propertyName);
                if (prop == null)
                    throw new Exception($"Failed to find property by name {propertyName}");
                return prop;
            }
        }

        private static string GetPropertyName<T>(this Expression<Func<T, object>> expression)
        {
            if (expression.Body is UnaryExpression unary)
                return ((MemberExpression)unary.Operand).Member.Name;
            return ((MemberExpression)expression.Body).Member.Name;
        }

    }
}
