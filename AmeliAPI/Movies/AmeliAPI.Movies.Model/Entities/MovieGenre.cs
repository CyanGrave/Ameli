using ModelBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmeliAPI.Movies.Model.Entities
{
    public class MovieGenre : EntityBase
    {
        [Key]
        [Column(Order = 0)]
        public int MovieID { get; set; }

        [ForeignKey(nameof(MovieID))]
        [InverseProperty("MovieGenres")]
        public virtual Movie Movie { get; set; }


        [Key]
        [Column(Order = 1)]
        public int GenreID { get; set; }

        [ForeignKey(nameof(GenreID))]
        [InverseProperty("MovieGenres")]
        public virtual Genre Genre { get; set; }
    }
}
