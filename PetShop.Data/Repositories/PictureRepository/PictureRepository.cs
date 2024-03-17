using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories.PictureRepository
{
    public class PictureRepository<T> : IPictureRepository<T> where T : class
    {
        private readonly PetShopDbContext _dbcontext;

        public PictureRepository(PetShopDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Add(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
            _dbcontext.SaveChanges();
        }

        public void DeleteAll()
        {
            var pictures = _dbcontext.Pictures.ToList();
            _dbcontext.Pictures.RemoveRange(pictures);
            _dbcontext.SaveChanges();
        }

        public void DeleteById(T entity)
        {
            if (entity != null)
            {
                _dbcontext.Set<T>().Remove(entity);
                _dbcontext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbcontext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dbcontext.Set<T>().Find(id);
        }

        public T GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            _dbcontext.SaveChanges();
        }
        public void DeleteByAnimalId(int animalId)
        {
            var pictures = _dbcontext.Pictures.Where(p => p.AnimalId == animalId).ToList();
            _dbcontext.Pictures.RemoveRange(pictures);
            _dbcontext.SaveChanges();
        }
    }
}
