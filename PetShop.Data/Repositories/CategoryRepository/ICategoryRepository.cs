using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories.CategoryRepository
{
    public interface ICategoryRepository<T> 
    {
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void DeleteById(T entity);
        IEnumerable<T> GetAll();
        void DeleteAll();
    }
}
