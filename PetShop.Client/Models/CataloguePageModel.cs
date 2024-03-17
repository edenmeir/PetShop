using PetShop.Data.Repositories;
using PetShop.Models.Entities;

namespace PetShop.Client.Models
{
    public class CataloguePageModel
    {
        public List<Animal> Animals { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<Category> Categories { get; set; }
        public List<Comment> Comments { get; set; }
    }

}
