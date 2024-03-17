using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Models.Entities;

public partial class Picture
{
    [Key]
    public int PictureId { get; set; }

    public int AnimalId { get; set; }

    public string PictureSrc { get; set; } = null!;

    [ForeignKey("AnimalId")]
    [InverseProperty("Pictures")]
    public virtual Animal Animal { get; set; } = null!;
}
