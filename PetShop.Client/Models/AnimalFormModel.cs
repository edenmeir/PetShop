using PetShop.Models.Entities;

namespace PetShop.Client.Models
{
    public class AnimalFormModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public byte[] Picture { get; set; }


        public string NameValidator { get; set; }
        public string AgeValidator { get; set; }
        public string DescriptionValidator { get; set; }

        public string CategoryValidator { get; set; }

        public string PictureValidator { get; set; }

        public List<Category> Categories { get; set; }
    }
}
