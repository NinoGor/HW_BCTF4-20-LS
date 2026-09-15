using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Movies.Domain.Entities
{
    public class StudioDetails
    {
        public int Id { get; set; }

        [Required]
        public string LicenseNumber { get; set; }

        // FK - კინოსტუდიის დეტალები ვერ იარსებებს კინოსტუდიის გარეშე, ამიტომ FK ამ მხარესაა
        public int StudioId { get; set; }

        // (1:1) კინოსტუდიის დეტალები კავშირშია ერთ კინოსტუდიასთან
        public Studio Studio { get; set; }

    }
}
