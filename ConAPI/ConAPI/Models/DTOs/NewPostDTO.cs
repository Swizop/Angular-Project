using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.DTOs
{
    public class NewPostDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
