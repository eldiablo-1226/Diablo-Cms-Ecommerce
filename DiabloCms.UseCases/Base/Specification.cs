using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DiabloCms.UseCases.Base
{
    public abstract class Specification<T> where T : class
    {
        private static readonly ConcurrentDictionary<string, Func<T, bool>> DelegateCache
            = new();

        private readonly List<string> _cacheKey;

        protected Specification()
        {
            _cacheKey = new List<string> {GetType().Name};
        }

        protected virtual bool Include => true;

        public virtual bool IsSatisfiedBy(T value)
        {
            if (!Include) return true;

            var func = DelegateCache.GetOrAdd(
                string.Concat(_cacheKey),
                _ => ToExpression().Compile());

            return func(value);
        }

        public Specification<T> And(Specification<T> specification)
        {
            if (!specification.Include) return this;

            _cacheKey.Add($"{nameof(And)}{specification.GetType()}");

            return new BinarySpecification(this, specification, true);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (!specification.Include) return this;

            _cacheKey.Add($"{nameof(Or)}{specification.GetType()}");

            return new BinarySpecification(this, specification, false);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification.Include
                ? specification.ToExpression()
                : _ => true;
        }

        private class BinarySpecification : Specification<T>
        {
            private readonly bool _andOperator;
            private readonly Specification<T> _left;
            private readonly Specification<T> _right;

            public BinarySpecification(Specification<T> left, Specification<T> right, bool andOperator)
            {
                _right = right;
                _left = left;
                _andOperator = andOperator;
            }

            public override Expression<Func<T, bool>> ToExpression()
            {
                Expression<Func<T, bool>> leftExpression = _left;
                Expression<Func<T, bool>> rightExpression = _right;

                Expression body = _andOperator
                    ? Expression.AndAlso(leftExpression.Body, rightExpression.Body)
                    : Expression.OrElse(leftExpression.Body, rightExpression.Body);

                var parameter = Expression.Parameter(typeof(T));
                body = (BinaryExpression) new ParameterReplacer(parameter).Visit(body);

                body = body ?? throw new InvalidOperationException("Binary expression cannot be null.");

                return Expression.Lambda<Func<T, bool>>(body, parameter);
            }
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter;

            internal ParameterReplacer(ParameterExpression parameter)
            {
                _parameter = parameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return base.VisitParameter(_parameter);
            }
        }
    }
}