using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Models.Entities;

public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    public int AnimalId { get; set; }

    [Column("Comment")]
    public string? Comment1 { get; set; }

    [ForeignKey("AnimalId")]
    [InverseProperty("Comments")]
    public virtual Animal Animal { get; set; } = null!;
}
