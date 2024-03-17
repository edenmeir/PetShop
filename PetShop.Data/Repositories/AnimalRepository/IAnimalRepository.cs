using PetShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories.AnimalRepository
{
    public interface IAnimalRepository<T> where T : class
    {
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(int id);
        IEnumerable<T> GetAll();
        void DeleteAll();
        IEnumerable<Animal> GetMostReviewedAnimals(int animalsCount);
        List<Animal> GetByCategoryId(int categoryId);
    }
}
