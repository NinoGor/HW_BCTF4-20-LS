using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string StudioName { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, Title={Title}, ReleaseYear={ReleaseYear}, StudioName={StudioName}";
        }
    }
}
