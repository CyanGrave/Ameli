using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelBase;

namespace AmeliAPI.UserManagement.Model.Entities
{
    public class User : EntityBase
    {
        [NotMapped]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public override long EntityTypeID { get { return GetSimpleHash(nameof(User)); } }



        [Required]
        [Key]
        [Column(Order = 0)]
        public int UserID { get; set; }



        [StringLength(50)]
        [Required]
        public string? UserName { get; set; }


        [Required]
        public string? PasswordHash { get; set; }
    }
}
