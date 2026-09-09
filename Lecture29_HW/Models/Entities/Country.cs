using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lecture29_HW.Models.Entities
{
    internal class Country
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // (1:M) ერთ ქვეყანაშუ შეიძლება იყოს ბევრი კინოსტუდია
        public ICollection<Studio> Studios { get; set; } = new List<Studio>();

    }
}
