using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Client.Models;
using PetShop.Data;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.AnimalRepository;
using PetShop.Data.Repositories.CategoryRepository;
using PetShop.Data.Repositories.PictureRepository;
using PetShop.Models.Entities;

namespace PetShop.Client.Controllers
{
    public class AdminController : Controller
    {

        private readonly ICategoryRepository<Category> _categoryRepository;
        private readonly IAnimalRepository<Animal> _animalRepository;
        private readonly IPictureRepository<Picture> _pictureRepository;
        public AdminController(ICategoryRepository<Category> categoryRepository, IAnimalRepository<Animal> animalRepository, IPictureRepository<Picture> pictureRepository)
        {
            _categoryRepository = categoryRepository;
            _animalRepository = animalRepository;
            _pictureRepository = pictureRepository;
        }
        public IActionResult Index()
        {
            var animals = _animalRepository.GetAll().ToList();
            var pictures = _pictureRepository.GetAll().ToList();
            var categories = _categoryRepository.GetAll().ToList();


            CataloguePageModel cataloguePageModel = new CataloguePageModel
            {
                Animals = animals,
                Pictures = pictures,
                Categories = categories
            };

            return View(cataloguePageModel);
        }

        public IActionResult Delete(int id)
        {
            _animalRepository.DeleteById(id);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var animal = _animalRepository.GetById(id);

            if(animal == null)
            {
                return NotFound();
            }

            var model = new AnimalViewModel
            {
                AnimalId = animal.AnimalId,
                Name = animal.Name,
                Age = animal.Age,
                Description = animal.Description,

            };

            return View(model);
        }

    }
}
