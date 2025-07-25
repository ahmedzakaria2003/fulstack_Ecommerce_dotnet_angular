﻿using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
  public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        // Property Signature for each dynamic part in query
        public Expression<Func<TEntity, bool>>? Criteria { get; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        #endregion

        #region Sorting

        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; }

        #endregion

        #region Pagination

        public int Take { get; }    

        public int Skip { get; }

        public bool IsPagingEnabled { get; }


        #endregion


    }
}
