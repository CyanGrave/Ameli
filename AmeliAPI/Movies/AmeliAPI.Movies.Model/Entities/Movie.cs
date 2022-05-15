using ModelBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmeliAPI.Movies.Model.Entities
{
    public class Movie : EntityBase
    {
        [Required]
        [Key]
        [Column(Order = 0)]
        public int MovieID { get; set; }



        [StringLength(100)]
        [Required]
        public string? MovieTitle { get; set; }
		
		[StringLength(100)]
        [Required]
        public string? MovieSubtitle { get; set; }


        public DateOnly ReleaseDate { get; set; }


        [InverseProperty("Movie")]
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

        [InverseProperty("Movie")]
        public virtual ICollection<MovieAssociatedPerson> MovieAssociatedPersons { get; set; }


    }
}
