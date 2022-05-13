using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Text;

namespace LambdaSerializer
{
    public static class Serializer
    {

        public static string SerializeFilter<TEntity>(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
                return string.Empty;

            LambdaToStringConverter converter = new LambdaToStringConverter();

            return converter.Convert(filter);
        }


        public static Expression<Func<TEntity, bool>>? DeserializeFilter<TEntity>(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return null;

            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity));

            LambdaExpression lambdaExpression = null;
            try
            {

                lambdaExpression = DynamicExpressionParser.ParseLambda(new[] { parameterExpression }, typeof(bool), expression);
            }
            catch (System.Linq.Dynamic.Core.Exceptions.ParseException ex)
            {
                try
                {
                    if (ex.Message.Count(chr => chr == '\'') >= 2)
                    {
                        string strParamName = ex.Message.Split(new char[] { '\'' })[1];

                        parameterExpression = Expression.Parameter(typeof(TEntity), strParamName.Trim());
                        lambdaExpression = DynamicExpressionParser.ParseLambda(new[] { parameterExpression }, typeof(bool), expression);
                    }
                }
                catch { }
            }

            if(lambdaExpression != null)
            {
                LambdaValidator validator = new LambdaValidator();
                if (!validator.IsValid(lambdaExpression))
                    return null;
                else
                    return lambdaExpression as Expression<Func<TEntity, bool>>;
            }

            return null;
        }


        private class LambdaToStringConverter : ExpressionVisitor
        {

            private static HashSet<Type> NumericTypes = new HashSet<Type>
            {
                typeof(int),
                typeof(uint),
                typeof(double),
                typeof(decimal),
                typeof(float),
                typeof(long),
                typeof(ulong)
            };

            internal static bool IsNumericType(Type type)
            {
                if (type is null)
                    return false;


                return NumericTypes.Contains(type) ||
                       (Nullable.GetUnderlyingType(type) as Type is not null && NumericTypes.Contains(type));
            }

            internal static bool IsEnumType(Type type)
            {
                return type.IsEnum || (Nullable.GetUnderlyingType(type)?.IsEnum?? false);
            }

            internal static bool IsDateTime(Type type)
            {
                return type == typeof(DateTime) || Nullable.GetUnderlyingType(type) == typeof(DateTime);
            }

            private string _lambdaString = "";

            public string Convert(Expression exp)
            {
                _lambdaString = exp.ToString();
                Visit(exp);

                return _lambdaString;
            }


            protected override Expression VisitMember(MemberExpression node)
            {
                if (ContainsConstant(node))
                {

                    string strNode = node.ToString();

                    var value = GetValue(node);

                    var strValue = value?.ToString()?? "null";


                    if (IsNumericType(node.Type) || value == null || strValue == "null")
                        _lambdaString = _lambdaString.Replace(strNode, strValue);
                    else if (IsEnumType(node.Type))
                    {
                        int enumValue = (int)value;

                        _lambdaString = _lambdaString.Replace(strNode, ((int)value).ToString());
                    }
                    else if (IsDateTime(node.Type))
                    {
                        _lambdaString = _lambdaString.Replace(strNode, "\"" + ((DateTime)value).ToString("yyyy/MM/dd HH:mm:ss") + "\"");
                    }
                    else if (node.Type == typeof(string))
                        _lambdaString = _lambdaString.Replace(strNode, "\"" + value.ToString() + "\"");


                }

                return base.VisitMember(node);
            }

            protected override Expression VisitUnary(UnaryExpression node)
            {

                if (node.NodeType == ExpressionType.Convert)
                {
                    string strNode = node.ToString();

                    _lambdaString = _lambdaString.Replace(strNode, node.Operand.ToString());
                }

                return base.VisitUnary(node);
            }



            private object GetValue(MemberExpression member)
            {
                var objectMember = Expression.Convert(member, typeof(object));

                var getterLambda = Expression.Lambda<Func<object>>(objectMember);

                var getter = getterLambda.Compile();

                return getter();
            }


            private bool ContainsConstant(MemberExpression member)
            {
                if (member is null)
                    return false;

                if (member.Expression is null)
                    return false;

                if (member.Expression.NodeType == ExpressionType.Constant)
                    return true;
                else if (member.Expression.NodeType == ExpressionType.MemberAccess)
                    return ContainsConstant(member.Expression as MemberExpression);

                return false;
            }

        }

        private class LambdaValidator : ExpressionVisitor
        {

            private bool _isValid = true;

            public bool IsValid(Expression expression)
            {
                Visit(expression);

                return _isValid;
            }

            protected override Expression VisitInvocation(InvocationExpression node)
            {
                _isValid = false;

                return base.VisitInvocation(node);
            }
        }
    }
}
