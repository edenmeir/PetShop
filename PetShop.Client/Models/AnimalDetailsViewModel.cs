using PetShop.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Client.Models
{
    public class AnimalDetailsViewModel
    {
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }
        public int? Age { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public List<Comment> Comments { get; set; }

        // New property for adding comments
        [Required(ErrorMessage = "Please enter a comment.")]
        public string CommentText { get; set; }
    }
}
