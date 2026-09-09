using System.ComponentModel.DataAnnotations;

namespace Lecture29_HW.Models.Entities
{
    internal class Actor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        // (M:N) მსახიობი შესაძლოა მონაწილეობდეს ბევრ ფილმში
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();

    }
}
