namespace PetShop.Client.Models
{
    public class AnimalViewModel
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? PictureSrc { get; set; }
    }
}
