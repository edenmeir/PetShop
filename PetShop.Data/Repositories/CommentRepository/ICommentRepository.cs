using PetShop.Models.Entities;
using System.Collections.Generic;

namespace PetShop.Data.Repositories
{
    public interface ICommentRepository<T>
    {
        List<Comment> GetCommentsByAnimalId(int animalId);
        void Add(Comment comment);
    }
}
