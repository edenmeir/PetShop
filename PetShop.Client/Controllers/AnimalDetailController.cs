using Microsoft.AspNetCore.Mvc;
using PetShop.Client.Models;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.AnimalRepository;
using PetShop.Models.Entities;

namespace PetShop.Client.Controllers
{
    public class AnimalDetailController : Controller
    {
        private readonly IAnimalRepository<Animal> _animalRepository;
        private readonly ICommentRepository<Comment> _commentRepository;

        public AnimalDetailController(IAnimalRepository<Animal> animalRepository, ICommentRepository<Comment> commentRepository)
        {
            _animalRepository = animalRepository;
            _commentRepository = commentRepository;
        }

        public IActionResult AnimalDetails(int animalId)
        {
            var animal = _animalRepository.GetById(animalId);
            if (animal == null)
            {
                return NotFound();
            }

            var comments = _commentRepository.GetCommentsByAnimalId(animalId);

            var viewModel = new AnimalDetailsViewModel
            {
                AnimalId = animal.AnimalId,
                AnimalName = animal.Name,
                Age = (int)animal.Age,
                Description = animal.Description,
                CategoryName = animal.Category?.Name,
                Comments = comments
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddComment(AnimalDetailsViewModel animalDetailsViewModel)
        {

            var animal = _animalRepository.GetById(animalDetailsViewModel.AnimalId);

            if (animal == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                AnimalId = animal.AnimalId,
                Comment1 = animalDetailsViewModel.CommentText
            };

            _commentRepository.Add(comment);

            return RedirectToAction("AnimalDetails", "Catalogue", new { animalId = animalDetailsViewModel.AnimalId });

        }
    }
}

