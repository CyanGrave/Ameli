using ModelBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmeliAPI.Movies.Model.Entities
{
    public class Genre : EntityBase
    {
        [Required]
        [Key]
        [Column(Order = 0)]
        public int GenreID { get; set; }



        [StringLength(100)]
        [Required]
        public string? Name { get; set; }


        [InverseProperty("Genre")]
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
