using Microsoft.EntityFrameworkCore;
using PetShop.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Data.Repositories
{
    public class CommentRepository<T> : ICommentRepository<T> where T : Comment
    {
        private readonly PetShopDbContext _dbContext;

        public CommentRepository(PetShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Comment> GetCommentsByAnimalId(int animalId)
        {
            return _dbContext.Comments
                .Where(c => c.AnimalId == animalId)
                .ToList();
        }

        public void Add(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

    }
}
