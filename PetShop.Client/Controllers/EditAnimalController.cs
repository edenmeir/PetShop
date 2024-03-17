using Microsoft.AspNetCore.Mvc;
using PetShop.Client.Models;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.AnimalRepository;
using PetShop.Data.Repositories.CategoryRepository;
using PetShop.Models.Entities;

namespace PetShop.Client.Controllers
{
    public class EditAnimalController : Controller
    {
        private readonly IAnimalRepository<Animal> _animalRepository;
        private readonly ICategoryRepository<Category> _categoryRepository;
        public EditAnimalController(IAnimalRepository<Animal> animalRepository, ICategoryRepository<Category> categoryRepository)
        {
            _animalRepository = animalRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View(new AnimalFormModel { Categories = _categoryRepository.GetAll().ToList() });
        }
        // GET: Animal/Edit/5
        public IActionResult Edit(int id)
        {
            var animal = _animalRepository.GetById(id);

            if (animal == null)
            {
                return NotFound();
            }

            var animalViewModel = new AnimalViewModel
            {
                AnimalId = animal.AnimalId,
                Name = animal.Name,
                Age = animal.Age,
                Description = animal.Description
            };

            return View(animalViewModel);
        }

        [HttpPost]
        public IActionResult SaveChanges(AnimalViewModel animalViewModel)
        {
            if (ModelState.IsValid)
            {
                var animal = _animalRepository.GetById(animalViewModel.AnimalId);

                if (animal == null)
                {
                    return NotFound();
                }

                animal.Name = animalViewModel.Name;
                animal.Age = animalViewModel.Age;
                animal.Description = animalViewModel.Description;

                _animalRepository.Update(animal);

                ViewBag.UpdateSuccess = true;

                return RedirectToAction("Index", "Admin");
            }

            return View(animalViewModel);
        }

    }
}
