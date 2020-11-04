using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TeachMe.Core.Utils
{
    /// <summary>
    /// 
    /// <remarks>http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx</remarks>
    /// </summary>
    public static class ExpressionExtension
    {
        /// <summary>
        /// 
        /// <remarks>http://www.albahari.com/nutshell/predicatebuilder.aspx</remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Query<T>() { return f => true; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="merge"></param>
        /// <returns></returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            //Armada para n colocar o true no SQL
            if (first.ToString().Equals("f => True"))
            {
                return second;
            }

            return first.Compose(second, Expression.OrElse);
        }

        /// <summary>
        /// Get Property Name os expression, help to make include in Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(this Expression<Func<T, object>> expression)
        {
            MemberExpression memberExpr = expression.Body as MemberExpression;
            if (memberExpr == null)
            {
                throw new ArgumentException(nameof(expression));
            }
            List<string> result = new List<string>();
            if (memberExpr.Expression != null)
            {
                var subExpression = memberExpr.Expression;
                while (subExpression != null)
                {
                    var subBody = subExpression as MemberExpression;
                    if (subBody != null)
                    {
                        result.Insert(0, subBody.Member.Name);
                        subExpression = subBody.Expression;
                    }
                    else
                    {
                        subExpression = null;
                    }

                }
            }
            result.Add(memberExpr.Member.Name);
            return string.Join(".", result);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        internal static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> mapper, Expression expression)
        {
            return new ParameterRebinder(mapper).Visit(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(node, out replacement))
            {
                node = replacement;
            }
            return base.VisitParameter(node);
        }
    }
}
