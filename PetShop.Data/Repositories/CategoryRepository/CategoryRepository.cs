using Microsoft.EntityFrameworkCore;
using PetShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories.CategoryRepository
{
    public class CategoryRepository<T> : ICategoryRepository<T> where T : class
    {
        private readonly PetShopDbContext _dbContext;

        public CategoryRepository(PetShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteAll()
        {
            var categories = _dbContext.Categories.ToList();
            _dbContext.Categories.RemoveRange(categories);
            _dbContext.SaveChanges();
        }

        public void DeleteById(T entity)
        {
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

    }
}
