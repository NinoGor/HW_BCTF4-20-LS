using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lecture29_HW.Models.Entities
{
    internal class Studio
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // FK
        public int CountryId { get; set; }

        // (1:M) კინოსტუდია კავშირშია ერთ ქვეყანასთან 
        public Country Country { get; set; }

        // (1:1) კინოსტუდია კავშირშია ერთ შესაბამის დეტალებთან
        public StudioDetails StudioDetails { get; set; }

        // (1:M) კინოსტუდიას შესაძლოა კავშირში იყოს ბევრ ფილმთან
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();

    }
}
