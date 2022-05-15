using ModelBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmeliAPI.Movies.Model.Entities
{
    public class AssociatedPerson : EntityBase
    {
        [Required]
        [Key]
        [Column(Order = 0)]
        public int AssociatedPersonID { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        public string Prename { get; set; }


        [InverseProperty("AssociatedPerson")]
        public virtual ICollection<MovieAssociatedPerson> MovieAssociatedPersons { get; set; }



    }
}
