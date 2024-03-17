using Microsoft.EntityFrameworkCore;
using PetShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories.AnimalRepository
{
    public class AnimalRepository<T> : IAnimalRepository<T> where T : class
    {
        private readonly PetShopDbContext _dbContext;

        public AnimalRepository(PetShopDbContext dbContext)
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
            var animals = _dbContext.Animals.ToList();
            _dbContext.Animals.RemoveRange(animals);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
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

        public List<Animal> GetByCategoryId(int categoryId)
        {
            var animals = _dbContext.Animals
                                .Where(a => a.CategoryId == categoryId)
                                .ToList();

            return animals;
        }
        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<Animal> GetMostReviewedAnimals(int animalsCount)
        {
            return _dbContext.Animals!.OrderByDescending(a => a.Comments!.Count).Take(animalsCount).ToList();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var pictures = _dbContext.Pictures.Where(p => p.AnimalId == id).ToList();
            _dbContext.Pictures.RemoveRange(pictures);

            var entity = _dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }
}
