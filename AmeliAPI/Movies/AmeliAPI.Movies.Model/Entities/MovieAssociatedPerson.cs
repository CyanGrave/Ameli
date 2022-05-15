using ModelBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmeliAPI.Movies.Model.Entities
{
    public enum RoleFlag : int
    {
        Actor = 1,
        Director = 2,
        Writer = 4,
    }

    public class MovieAssociatedPerson : EntityBase
    {
        [Key]
        [Column(Order = 0)]
        public int MovieID { get; set; }

        [ForeignKey(nameof(MovieID))]
        [InverseProperty("MovieAssociatedPersons")]
        public virtual Movie Movie { get; set; }


        [Key]
        [Column(Order = 1)]
        public int AssociatedPersonID { get; set; }

        [ForeignKey(nameof(AssociatedPersonID))]
        [InverseProperty("MovieAssociatedPersons")]
        public virtual AssociatedPerson AssociatedPerson { get; set; }

        [Required]
        public RoleFlag RoleFlags { get; set; }
    }
}
