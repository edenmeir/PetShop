using Microsoft.AspNetCore.Mvc;
using PetShop.Client.Models;
using PetShop.Data;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.AnimalRepository;
using PetShop.Data.Repositories.CategoryRepository;
using PetShop.Data.Repositories.PictureRepository;
using PetShop.Models.Entities;

namespace PetShop.Client.Controllers
{


    public class CatalogueController : Controller
    {
        private readonly ICategoryRepository<Category> _categoryRepository;
        private readonly IAnimalRepository<Animal> _animalRepository;
        private readonly IPictureRepository<Picture> _pictureRepository;
        private readonly ICommentRepository<Comment> _commentRepository;
        public CatalogueController(ICategoryRepository<Category> categoryRepository, IAnimalRepository<Animal> animalRepository,
            IPictureRepository<Picture> pictureRepository, ICommentRepository<Comment> commentRepository)
        {
            _categoryRepository = categoryRepository;
            _animalRepository = animalRepository;
            _pictureRepository = pictureRepository;
            _commentRepository = commentRepository;
        }

        public IActionResult Index(int? categoryId)
        {
            List<Animal> animals;

            if (categoryId.HasValue)
            {
                animals = _animalRepository.GetByCategoryId(categoryId.Value).ToList();
            }
            else
            {
                animals = _animalRepository.GetAll().ToList();
            }

            var model = new CataloguePageModel
            {
                Animals = animals,
                Categories = _categoryRepository.GetAll().ToList(),
                Pictures = _pictureRepository.GetAll().ToList()
            };

            return View(model);
        }
        public IActionResult AnimalDetails(int animalId)
        {
            var animal = _animalRepository.GetById(animalId);
            if (animal == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById((int)animal.CategoryId);
            var comments = _commentRepository.GetCommentsByAnimalId(animalId).ToList();

            var model = new AnimalDetailsViewModel
            {
                AnimalId = animalId,
                AnimalName = animal.Name,
                Age = animal.Age,
                Description = animal.Description,
                CategoryName = category != null ? category.Name : string.Empty,
                Comments = comments
            };

            return View(model);
        }
    }
}

