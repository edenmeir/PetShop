using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Models.Entities;

public partial class Animal
{
    [Key]
    public int AnimalId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int? Age { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Animals")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Animal")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Animal")]
    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
}
