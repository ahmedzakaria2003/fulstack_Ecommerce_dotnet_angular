﻿using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
  public  abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {


        protected BaseSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExpression)
        {
            IncludeExpressions.Add(IncludeExpression);
        }
        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
        #endregion

        #region Pagination
        public int Take { get; private set; } 

        public int Skip { get; private set; } 

        public bool IsPagingEnabled { get; private set; } = false;

        public void ApplyPaging(int PageSize , int PageIndex)
        {
            Skip = (PageIndex - 1 )* PageSize;
            Take = PageSize;
            IsPagingEnabled = true;
        }


        #endregion

    }
}
