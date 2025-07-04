using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = new();  
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // Get Type Name
            var typeName = typeof(TEntity).Name;
            //Dictionary<string, object> ==> String Key [name of type] -- > Object Value [Repository]
            if(_repositories.ContainsKey(typeName))
            {
                return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            }
            else
            {
                // Create a new repository and add it to the dictionary
                var repoistory = new GenericRepository<TEntity, TKey>(_dbContext);
                // store the repository in the dictionary
                _repositories.Add(typeName, repoistory);
                // return repoistory;
                return repoistory;

            }
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
