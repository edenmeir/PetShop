using Microsoft.AspNetCore.Mvc;
using PetShop.Client.Models;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.AnimalRepository;
using PetShop.Data.Repositories.CategoryRepository;
using PetShop.Data.Repositories.PictureRepository;
using PetShop.Models.Entities;
using System.Drawing.Text;

namespace PetShop.Client.Controllers
{
    public class AddAnimalController : Controller
    {

        private readonly IAnimalRepository<Animal> _animalRepository;
        private readonly ICategoryRepository<Category> _categoryRepository;
        private readonly IPictureRepository<Picture> _pictureRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AddAnimalController(IAnimalRepository<Animal> animalRepository, ICategoryRepository<Category> categoryRepository,
            IPictureRepository<Picture> pictureRepository, IWebHostEnvironment webHostEnvironment)
        {
            _animalRepository = animalRepository;
            _categoryRepository = categoryRepository;
            _pictureRepository = pictureRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(new AnimalFormModel { Categories = _categoryRepository.GetAll().ToList() });
        }


        [HttpPost]
        public IActionResult Index(AnimalFormModel animal)
        {
            bool isValid = true;

            AnimalFormModel animalFormValidatorModel = new AnimalFormModel()
            {
                Categories = _categoryRepository.GetAll().ToList(),
                Name = animal.Name,
                Age = animal.Age,
                Description = animal.Description,
                Category = animal.Category,
                Picture = animal.Picture,
            };

            
            if (animal.Name == null || animal.Name == "")
            {
                animalFormValidatorModel.NameValidator = "Please provide name";
                isValid = false;
            }

            if (animal.Age == 0 || animal.Age.ToString() == null)
            {
                animalFormValidatorModel.AgeValidator = "Please provide age";
                isValid = false;
            }

            if (animal.Description == null || animal.Description == "")
            {
                animalFormValidatorModel.DescriptionValidator = "Please provide a description";
                isValid = false;
            }

            if (animal.Category == null || animal.Category.ToString() == "--- Select Category ---")
            {
                animalFormValidatorModel.CategoryValidator = "Please provide a category";
                isValid = false;
            }

            Picture picture = null;
            foreach (IFormFile file in Request.Form.Files)
            {
                if (file.Length > 0)
                {
                    string path = $"{_webHostEnvironment.WebRootPath}\\images\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string filename = file.FileName.ToLower();
                    string filePath = Path.Combine($"{path}", filename);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        file.CopyToAsync(fileStream);
                    }
                    picture = new Picture()
                    {
                        PictureSrc = $"/images/{filename}"
                    };
                }
            }

            if (picture == null)
            {
                animalFormValidatorModel.PictureValidator = "Please provide a picture";
                isValid = false;
            }

            if (isValid)
            {
                var newAnimal = new Animal()
                {
                    Name = animal.Name,
                    Age = animal.Age,
                    Description = animal.Description,
                    CategoryId = animal.Category
                };
                _animalRepository.Add(newAnimal);

                picture.AnimalId = newAnimal.AnimalId;

                _pictureRepository.Add(picture);
            }

            return View(animalFormValidatorModel);
        }
    }
}
