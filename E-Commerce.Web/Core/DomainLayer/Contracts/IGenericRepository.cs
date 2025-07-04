using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        Task <IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetbyIdAsync(TKey id);

        #region With Specification

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey>specifications);

        Task<TEntity?> GetbyIdAsync(ISpecification<TEntity, TKey> specifications);


        #endregion

        Task <int> CountAsync(ISpecification<TEntity, TKey> specifications);  
    }
}
