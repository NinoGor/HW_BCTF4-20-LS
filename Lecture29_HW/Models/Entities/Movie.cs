using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lecture29_HW.Models.Entities
{
    internal class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        // FK
        public int StudioId { get; set; }

        // (1:M) ფილმი კავშირშია ერთ კინოსტუდიასთან
        public Studio Studio { get; set; }

        // (M:N) ფილმში შესაძლოა მონაწილეობდეს ბევრი მსახიობი
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();

    }
}
