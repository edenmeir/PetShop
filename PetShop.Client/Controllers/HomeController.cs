using Microsoft.AspNetCore.Mvc;
using PetShop.Client.Models;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.AnimalRepository;
using PetShop.Data.Repositories.PictureRepository;
using PetShop.Models.Entities;
using System.Diagnostics;

namespace PetShop.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimalRepository<Animal> _animalRepository;
        private readonly IPictureRepository<Picture> _pictureRepository;
        private readonly ICommentRepository<Comment> _commentRepository;
        public HomeController(IAnimalRepository<Animal> animalRepository, IPictureRepository<Picture> pictureRepository, ICommentRepository<Comment> commentRepository)
        {
            _animalRepository = animalRepository;
            _pictureRepository = pictureRepository;
            _commentRepository = commentRepository;
        }

        public IActionResult Index()
        {
            var animals = _animalRepository.GetMostReviewedAnimals(2);

            var model = new CataloguePageModel
            {
                Animals = (List<Animal>)animals,
                Pictures = _pictureRepository.GetAll().ToList(),
            };

            foreach (var animal in model.Animals)
            {
                animal.Comments = _commentRepository.GetCommentsByAnimalId(animal.AnimalId);
            }

            return View(model);
        }

    }
}