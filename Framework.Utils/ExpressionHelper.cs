using Framework.Utils.Anotations.DtoAnotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framework.Utils
{
    class LeftJoinIntermediate<TOuter, TInner>
    {
        public TOuter OneOuter { get; set; }
        public IEnumerable<TInner> ManyInners { get; set; }
    }

    class Replacer : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParam;
        private readonly Expression _replacement;

        public Replacer(ParameterExpression oldParam, Expression replacement)
        {
            _oldParam = oldParam;
            _replacement = replacement;
        }

        public override Expression Visit(Expression exp)
        {
            if (exp == _oldParam)
            {
                return _replacement;
            }

            return base.Visit(exp);
        }
    }
    public static class Extensions
    {
        public static List<T> RemoveAll<T>(this IList<T> lst,Func<T, bool> predicate)
        {
            List<T> result = new List<T>();
            foreach(var item in lst)
            {
                if(!predicate.Invoke(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
        public static List<T> Clone<T>(this IList<T> listToClone)
        {
            var result = new List<T>();
            foreach(var item in listToClone)
            {
                result.Add(item);
            }
            return result;
        }
    }

    public static class ExpressionHelper
    {

        /// <summary>
        /// Select expression from tSource to tResult
        /// tSource => new TResult(){ x = tSource.x }
        /// </summary>
        /// <typeparam name="TSource">typeof tSource</typeparam>
        /// <typeparam name="TResult">typeof tResult</typeparam>
        /// <returns></returns>
        public static Expression<Func<TSource, TResult>> SelectExpression<TSource, TResult>()
        {
            var tResult = typeof(TResult);
            var tSource = typeof(TSource);
            // input parameter "o"
            var xParameter = Expression.Parameter(tSource, "o");

            // new statement "new Data()"
            var xNew = Expression.New(tResult);

            // create initializers
            var bindings = tResult.GetProperties().Select(x => x.Name.Trim())
                .Select(o =>
                {
                    // property 
                    var mi = tResult.GetProperty(o);

                    var notForMap = mi.GetCustomAttributes(typeof(NotForMapAttribute), false).SingleOrDefault();
                    if (notForMap != null)
                    {
                        return null;
                    }

                    String srcColumnName = o;

                    var mapColumn = (MapColumnAttribute)mi.GetCustomAttributes(typeof(MapColumnAttribute), false).SingleOrDefault();
                    if (mapColumn != null)
                    {
                        srcColumnName = mapColumn.ColumnName;
                    }


                    var dt = tSource.GetProperty(srcColumnName);
                    if (dt == null)
                    {
                        return null;
                    }

                    // original value
                    var xOriginal = Expression.Property(xParameter, dt);


                    // set value 
                    return Expression.Bind(mi, xOriginal);
                }
            ).Where(o => o != null);


            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<TSource, TResult>>(xInit, xParameter);

            // compile to Func<Data, Data>
            return lambda;
        }

        //public static Func<T, object> GetQoutationByFunct<T>(string sortColumn)
        //{
        //    Func<T, object> QoutationByExpr = null;
        //    if (!String.IsNullOrEmpty(sortColumn))
        //    {
        //        Type sponsorResultType = typeof(T);

        //        if (sponsorResultType.GetProperties().Any(prop => prop.Name == sortColumn))
        //        {
        //            System.Reflection.PropertyInfo pinfo = sponsorResultType.GetProperty(sortColumn);
        //            QoutationByExpr = (data => pinfo.GetValue(data, null));
        //        }
        //    }
        //    return QoutationByExpr;
        //}

        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string key, bool ascending = true)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return query;
            }

            var lambda = (dynamic)CreateExpression(typeof(TSource), key);

            return ascending
                ? Queryable.OrderBy(query, lambda)
                : Queryable.OrderByDescending(query, lambda);
        }

        private static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");

            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }

            return Expression.Lambda(body, param);
        }

        public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> t)
        {
            return t.Select(SelectExpression<TSource, TResult>());
        }
        public static Expression<Func<TOuter, TInner, TResult>> JoinSelectResulExpression<TOuter, TInner, TResult>()
        {
            Type tResult = typeof(TResult);
            Type tInner = typeof(TInner);
            Type tOuter = typeof(TOuter);

            String tInnerName = tInner.Name;
            String tOuterName = tOuter.Name;

            HashSet<string> resultTypeNames = tResult.GetProperties().Select(x => x.Name).ToHashSet();
            string[] outerTypeNames = resultTypeNames.Where(x => x.StartsWith(tInnerName)).ToArray();
            string[] innerTypeNames = resultTypeNames.Where(x => x.StartsWith(tOuterName)).ToArray();

            var outerParam = Expression.Parameter(tOuter, "tOuter");
            var innerParam = Expression.Parameter(tInner, "tInner");

            // new statement "new Data()"
            var xNew = Expression.New(tResult);

            // create initializers
            var bindings = tResult.GetProperties().Select(x =>
                {
                    if (!x.CanWrite)
                        return null;

                    var resultTypeName = x.Name.Trim();
                    
                    // property 
                    var resultProperty = tResult.GetProperty(resultTypeName);

                    var notForMap = resultProperty.GetCustomAttributes(typeof(NotForMapAttribute), false).SingleOrDefault();
                    if (notForMap != null)
                    {
                        return null;
                    }

                    String srcColumnName = resultTypeName;
                    var mapColumn = (MapColumnAttribute)resultProperty.GetCustomAttributes(typeof(MapColumnAttribute), false).SingleOrDefault();
                    if (mapColumn != null)
                    {
                        srcColumnName = mapColumn.ColumnName;
                    }

                    PropertyInfo dt = null;

                    //type of outter name
                    if (srcColumnName.StartsWith(tOuterName))
                    {
                        dt = tOuter.GetProperty(srcColumnName.Substring(tOuterName.Length));
                        if (dt == null)
                            return null;
                        // original value
                        var xOriginal = Expression.Property(outerParam, dt);
                        // set value 
                        return Expression.Bind(resultProperty, xOriginal);
                    }

                    var i = tInnerName.IndexOf('`');

                    if (i >= 0)
                        tInnerName = tInnerName.Substring(0, i);

                    if (srcColumnName.StartsWith(tInnerName))
                    {
                        dt = tInner.GetProperty(srcColumnName.Substring(tInnerName.Length));
                        // original value
                        var xOriginal = Expression.Property(innerParam, dt);
                        // set value 
                        return Expression.Bind(resultProperty, xOriginal);
                    }
                    return null;
                }
            ).Where(o => o != null);


            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<TOuter, TInner, TResult>>(xInit, new ParameterExpression[] {
                outerParam,
                innerParam
            });

            // compile to Func<Data, Data>
            return lambda;
        }

        public static IQueryable<TSource> SelectToJoin<TSource, TResult>(this IQueryable<TSource> t)
        {
            var tResult = typeof(TResult);
            var tSource = typeof(TSource);
            // input parameter "o"
            var xParameter = Expression.Parameter(tSource, "o");
            // new statement "new Data()"
            var xNew = Expression.New(tSource);
            var tproperties = tResult.GetProperties();
            bool isSetId = false;
            // create initializers
            var bindings = tproperties.Select(x => x.Name.Trim())
                .Select(o =>
                {
                    // property 
                    var mi = tResult.GetProperty(o);
                    var notForMap = mi.GetCustomAttributes(typeof(NotForMapAttribute), false).SingleOrDefault();
                    if (notForMap != null)
                    {
                        return null;
                    }
                    String srcColumnName = o;
                    var mapColumn = (MapColumnAttribute)mi.GetCustomAttributes(typeof(MapColumnAttribute), false).SingleOrDefault();
                    if (mapColumn != null)
                    {
                        srcColumnName = mapColumn.ColumnName;
                    }
                    if (!srcColumnName.StartsWith(tSource.Name))
                    {
                        if (!isSetId)
                        {
                            var idDt = tSource.GetProperty("Id");
                            var xIdOriginal = Expression.Property(xParameter, idDt);
                            isSetId = true;
                            return Expression.Bind(idDt, xIdOriginal);
                        }
                        return null;
                    }
                    var dt = tSource.GetProperty(srcColumnName.Substring(tSource.Name.Length));
                    if (dt == null)
                    {
                        return null;
                    }
                    // original value
                    var xOriginal = Expression.Property(xParameter, dt);
                    // set value 
                    return Expression.Bind(dt, xOriginal);
                }
            ).Where(o => o != null);


            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);
            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<TSource, TSource>>(xInit, xParameter);
            // compile to Func<Data, Data>
            return t.Select(lambda);
        }

        public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
        this IQueryable<TOuter> outer,
        IQueryable<TInner> inner,
        Expression<Func<TOuter, TKey>> outerKeySelector,
        Expression<Func<TInner, TKey>> innerKeySelector,
        Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {
            MethodInfo groupJoin = typeof(Queryable).GetMethods()
                                                     .Single(m => m.ToString() == "System.Linq.IQueryable`1[TResult] GroupJoin[TOuter,TInner,TKey,TResult](System.Linq.IQueryable`1[TOuter], System.Collections.Generic.IEnumerable`1[TInner], System.Linq.Expressions.Expression`1[System.Func`2[TOuter,TKey]], System.Linq.Expressions.Expression`1[System.Func`2[TInner,TKey]], System.Linq.Expressions.Expression`1[System.Func`3[TOuter,System.Collections.Generic.IEnumerable`1[TInner],TResult]])")
                                                     .MakeGenericMethod(typeof(TOuter), typeof(TInner), typeof(TKey), typeof(LeftJoinIntermediate<TOuter, TInner>));
            MethodInfo selectMany = typeof(Queryable).GetMethods()
                                                      .Single(m => m.ToString() == "System.Linq.IQueryable`1[TResult] SelectMany[TSource,TCollection,TResult](System.Linq.IQueryable`1[TSource], System.Linq.Expressions.Expression`1[System.Func`2[TSource,System.Collections.Generic.IEnumerable`1[TCollection]]], System.Linq.Expressions.Expression`1[System.Func`3[TSource,TCollection,TResult]])")
                                                      .MakeGenericMethod(typeof(LeftJoinIntermediate<TOuter, TInner>), typeof(TInner), typeof(TResult));

            var groupJoinResultSelector = (Expression<Func<TOuter, IEnumerable<TInner>, LeftJoinIntermediate<TOuter, TInner>>>)
                                          ((oneOuter, manyInners) => new LeftJoinIntermediate<TOuter, TInner> { OneOuter = oneOuter, ManyInners = manyInners });

            MethodCallExpression exprGroupJoin = Expression.Call(groupJoin, outer.Expression, inner.Expression, outerKeySelector, innerKeySelector, groupJoinResultSelector);

            var selectManyCollectionSelector = (Expression<Func<LeftJoinIntermediate<TOuter, TInner>, IEnumerable<TInner>>>)
                                               (t => t.ManyInners.DefaultIfEmpty());

            ParameterExpression paramUser = resultSelector.Parameters.First();

            ParameterExpression paramNew = Expression.Parameter(typeof(LeftJoinIntermediate<TOuter, TInner>), "t");
            MemberExpression propExpr = Expression.Property(paramNew, "OneOuter");

            LambdaExpression selectManyResultSelector = Expression.Lambda(new Replacer(paramUser, propExpr).Visit(resultSelector.Body), paramNew, resultSelector.Parameters.Skip(1).First());

            MethodCallExpression exprSelectMany = Expression.Call(selectMany, exprGroupJoin, selectManyCollectionSelector, selectManyResultSelector);

            return outer.Provider.CreateQuery<TResult>(exprSelectMany);
        }

        public static Expression<Func<TOuter, TInner, TOuter>> JoinSelectResulExpression<TOuter, TInner>()
        {
            Type tResult = typeof(TOuter);
            Type tInner = typeof(TInner);
            Type tOuter = typeof(TOuter);

            String tInnerName = tInner.Name;
            String tOuterName = tOuter.Name;

            HashSet<string> resultTypeNames = tResult.GetProperties().Select(x => x.Name).ToHashSet();
            string[] outerTypeNames = resultTypeNames.Where(x => x.StartsWith(tInnerName)).ToArray();
            string[] innerTypeNames = resultTypeNames.Where(x => x.StartsWith(tOuterName)).ToArray();

            var outerParam = Expression.Parameter(tOuter, "tOuter");
            var innerParam = Expression.Parameter(tInner, "tInner");

            // new statement "new Data()"
            var xNew = Expression.New(tResult);

            // create initializers
            var bindings = tResult.GetProperties().Select(x => x.Name.Trim())
                .Select(resultTypeName =>
                {
                    // property 
                    var resultProperty = tResult.GetProperty(resultTypeName);

                    var notForMap = resultProperty.GetCustomAttributes(typeof(NotForMapAttribute), false).SingleOrDefault();
                    if (notForMap != null)
                    {
                        return null;
                    }

                    String srcColumnName = resultTypeName;
                    var mapColumn = (MapColumnAttribute)resultProperty.GetCustomAttributes(typeof(MapColumnAttribute), false).SingleOrDefault();
                    if (mapColumn != null)
                    {
                        srcColumnName = mapColumn.ColumnName;
                    }

                    PropertyInfo dt = null;

                    MemberExpression xOriginal = null;

                    if (srcColumnName.StartsWith(tInnerName))
                    {
                        dt = tInner.GetProperty(srcColumnName.Substring(tInnerName.Length));
                        // original value
                        xOriginal = Expression.Property(innerParam, dt);
                        // set value 
                        return Expression.Bind(resultProperty, xOriginal);
                    }
                    else
                    {
                        dt = tOuter.GetProperty(resultTypeName);
                        // original value
                        xOriginal = Expression.Property(outerParam, dt);
                        // set value 
                        return Expression.Bind(resultProperty, xOriginal);
                    }
                }
            ).Where(o => o != null);


            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<TOuter, TInner, TOuter>>(xInit, new ParameterExpression[] {
                outerParam,
                innerParam
            });

            // compile to Func<Data, Data>
            return lambda;
        }
    }
}
